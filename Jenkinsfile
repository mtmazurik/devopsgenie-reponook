pipeline {
  environment {
    registry = "mtmazurik/devopsgenie-reponook"
    registryCredential = 'DockerHub'
  }
  agent any
  stages {
    stage('reponook') {
      steps {
        git 'https://github.com/mtmazurik/devopsgenie-reponook.git'
      }
    }
    stage('Building image') {
      steps{
        script {
          dockerImage = docker.build registry + ":$BUILD_NUMBER"
        }
      }
    }
    stage('Deploy Image') {
      steps{
        script {
          docker.withRegistry( '', registryCredential) {
            dockerImage.push()
          }
        }
      }
    }
  }
}