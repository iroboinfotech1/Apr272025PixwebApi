# Docker Commands:
Use Below commands to build the image after testing the functionality 

### To build the Image :

`docker build --progress=plain -t arunalagesan/smsapi:latest -t smsapi .`

Dont ignore the . at the  end of the statement. It denotes to look for the docker file in the current directory

### To Spin up the container from the image:

`docker run -d --name smsapi -p 3001:80 arunalagesan/smsapi`

==arunalagesan== is my personal Docker hub account. Replace it accordingly with your account or the Org account

And launch the brower and nabigate to http://localhost:3001/swagger/index.html

**Note :**
If there is already a container running with arunalagesan/smsapi or smsapi stop and remove the container before running the docker build or docker run statment described above.

`docker stop smsapi`
and 
`docker rm smsapi`


