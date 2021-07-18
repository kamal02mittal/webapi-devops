pipeline {
    agent any

    environment {
        SonarQubeScanner = tool name: 'SonarQubeScanner', type: 'hudson.plugins.sonar.SonarRunnerInstallation'
        def app = ''
        def imgName = "i-kamal02-master"
        def contName = "c-kamal02-master"
        def dockerHubUsername = "kamalmittal2020"
    }

    stages {
        stage('Checkout Source Code') {
            steps {
                echo "Clean old checkout from workspace"
                cleanWs()
                echo "Source Code Checkout start"
                checkout scm
                echo "Source Code Checkout stop"
            }
        }

        stage("Restore Nuget Packages"){
            steps{
                bat "dotnet restore ${workspace}\\WebApplication2\\WebApplication2.csproj"
            }
        }

        stage('Clean'){
            steps{
                echo "clean starts here"
                // Installed MSBuild plugin and added path C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe
                //{workspace} is default variable of Jenkins. It will provide path to project in jenkis
                bat "dotnet clean ${workspace}\\WebApplication2\\WebApplication2.csproj"
            }
        }

        stage('Build'){
            steps{
                echo "Build starts here"
                // Installed MSBuild plugin and added path C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe
                //{workspace} is default variable of Jenkins. It will provide path to project in jenkis
                bat "dotnet build ${workspace}\\WebApplication2.sln"
            }
        }

        stage('Unit Test'){
            steps{
                script{
                    def nunit = "${workspace}\\NUnit\\extension\\netcoreapp3.1\\nunit3-console.exe"
                    echo "Unit Test Starts Here"
                    bat "\"${nunit}\" --result=NUnitResults.xml ${workspace}\\TestProject1\\bin\\Debug\\netcoreapp3.1\\TestProject1.dll"
                }
            }
        }

        stage('Run SonarQube Analysis'){
            steps{
                withSonarQubeEnv(installationName: 'Test_Sonar')  
                 {
                    //Scan Modules  
                    echo 'Sonarqube scanning started'                               
                    bat  """ ${SonarQubeScanner} -Dsonar.projectKey=using_jenkins -Dsonar.projectname=using_jenkins -Dsonar.sourceEncoding=UTF-8 -Dsonar.sources=${workspace}\\WebApplication2 -Dsonar.cs.nunit.reportsPaths=${workspace}\\NUnitResults.xml -Dsonar.verbose=true """
                 }
            }
        }

        stage('Create Docker Image'){
            steps{
                script{
                    bat "cd ${workspace}\\WebApplication2"
                    app = docker.build("${imgName}","-f ${workspace}\\WebApplication2\\Dockerfile .")
                }
            }
        }

        stage('Push Docker Image to Docker Hub'){
            steps{
                script {
                    withDockerRegistry(credentialsId: 'dockercredentials', url: 'https://registry.hub.docker.com'){
                        echo "tag docker image"
                        bat "docker tag ${imgName} ${dockerHubUsername}/${imgName}"
                        echo "push docker image"
                        bat "docker push ${dockerHubUsername}/${imgName}:latest"
                        echo "delete tagged docker image from local"
                        bat "docker rmi ${dockerHubUsername}/${imgName}"
                    }
                }
            }
        }

        stage("Deploy Docker on port 7100"){
            steps{
                script {
                    bat "docker run -d --name ${contName} -p 7100:7100 ${imgName}";
                }
            }
        }
    }
}