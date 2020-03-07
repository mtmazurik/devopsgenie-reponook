pipeline {
  environment {
    registry = "k8s-ubuntu"
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
          docker.withRegistry( 'mtmazurik/devopsgenie-reponook') {
            dockerImage.push()
          }
        }
      }
    }
  }
}