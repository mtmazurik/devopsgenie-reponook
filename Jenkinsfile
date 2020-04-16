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
          dockerImage = docker.build registry + ":latest"
        }
      }
    }
    stage('Deploy image') {
      steps{
        script {
          docker.withRegistry( '', registryCredential) {
            dockerImage.push()
          }
        }
      }
    }
    stage('New deployment') {
      steps{
        script {
          kubernetesDeploy(
              kubeconfigId: 'k8s-ubuntu',
              configs: 'devopsgenie-reponook*.yml'
          )
        }
      }
    }
  }
}