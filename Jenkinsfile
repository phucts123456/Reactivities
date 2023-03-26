pipeline {
  agent any
  options {
    buildDiscarder(logRotator(numToKeepStr: '2'))
  }
  environment {
    DOCKERHUB_CREDENTIALS = credentials('dockerhub2')
  }
  stages {
    stage('Check git repo'){
      steps{
        checkout scmGit(branches: [[name: '*/main']], extensions: [], userRemoteConfigs: [[url: 'https://github.com/phucts123456/Reactivities']])
      }
    }
    stage('Build') {
      steps {
        script{
         sh 'docker build -t 37180/reactivities-be:${BUILD_NUMBER} .'
        }
      }
    }
    stage('Login') {
      steps {
        sh 'echo $DOCKERHUB_CREDENTIALS_PSW | docker login -u $DOCKERHUB_CREDENTIALS_USR --password-stdin'
      }
    }
    stage('Push') {
      steps {
        sh 'docker push 37180/reactivities-be:${BUILD_NUMBER}'
      }
    }  
    stage('Pull and Run Image') {
        steps {
            sshagent(['ssh-remote-reactivities']) {
                sh 'ssh -o StrictHostKeyChecking=no -l phuchoquang ${REACTIVITIES_SERVER} docker rm --force api'
                sh 'ssh -o StrictHostKeyChecking=no -l phuchoquang ${REACTIVITIES_SERVER} docker image rm --force 37180/reactivities-be:${BUILD_NUMBER}'
                 sh 'ssh -o StrictHostKeyChecking=no -l phuchoquang ${REACTIVITIES_SERVER} docker pull 37180/reactivities-be:${BUILD_NUMBER}'
                 sh 'ssh -o StrictHostKeyChecking=no -l phuchoquang ${REACTIVITIES_SERVER} docker run -p 8000:80 -d --rm --name api -e router.sticky_session_cookie_names=JSESSIONID --network reactivities 37180/reactivities-be:${BUILD_NUMBER}'
                
            }
        }
    }
  }
}