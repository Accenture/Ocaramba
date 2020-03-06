echo '********************************************Executing tests on Docker********************************************'

docker-switch-linux
         
docker info
         
docker pull ocaramba/selenium
         
docker build -t ocaramba/selenium -f DockerfileBuild .
         
docker ps -a