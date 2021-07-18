pipeline {
    agent any

    environment {
        SonarQubeScanner = tool name: 'SonarQubeScanner', type: 'hudson.plugins.sonar.SonarRunnerInstallation'
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
                    def nunit = "${workspace}\\NUnit\\bin\\netcoreapp3.1\\nunit3-console.exe"
                    echo "Unit Test Starts Here"
                    bat '\"${nunit}\" --result=NUnitResults.xml "${workspace}\\TestProject1\\bin\\Debug\\netcoreapp3.1\\TestProject1.dll"'
                }
            }
        }

        stage('Run SonarQube Analysis'){
            steps{
                withSonarQubeEnv(installationName: 'SonarQubeServer')  
                 {
                    //Scan Modules  
                    echo 'Sonarqube scanning started'                               
                    bat  """ ${SonarQubeScanner} -Dsonar.projectKey=using_jenkins -Dsonar.projectname=using_jenkins -Dsonar.sourceEncoding=UTF-8 -Dsonar.sources=${workspace}\\WebApplication1 -Dsonar.cs.nunit.reportsPaths=${workspace}\\NUnitResults.xml -Dsonar.verbose=true """
                 }
            }
        }
    }
}