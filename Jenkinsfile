pipeline {
  environment {
    registry = "10.1.1.13:5000"
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
          docker.withRegistry( 'https://10.1.1.13:5000') {
            dockerImage.push()
          }
        }
      }
    }
  }
}