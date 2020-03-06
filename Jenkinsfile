pipeline {
  environment {
    registry = "k8s-ubuntu:5000"
    dockerImage = ''
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
          dockerImage = docker.build registry + "/devopsgenie-reponook:dev" 
        }
      }
    }
    stage('Deploy Image') {
      steps{
        script {
          docker.withRegistry( 'https://k8s-ubuntu:5000') {
            dockerImage.push()
          }
        }
      }
    }
  }
}