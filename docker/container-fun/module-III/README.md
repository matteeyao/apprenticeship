# Character Generator

So now that we know you can run a shell within a Docker container let's have some fun w/ it. Here is a simple script that will generate all the information for a StarWars character. Try running it in your terminal. (If you run into an error for `wget missing` you will have have to do  a quick `brew install wget`)

```zsh
while :
do
    wget -qO- https://swapi.co/api/people/?search=r2
    printf '\n'
    sleep 5s
done
```

Okay so now that we know that a docker container can run a shell within it, it stands to reason we could also run a shell script.

Let's get to it:

1. Run a container based off of the `alpine` image, version 3.7.3

    * The `alpine` image is a Linux distribution that is very popular among Docker images b/c it is only 5 MB in size

2. Name the container something indicative like "characters"

3. Run the container in `detached` mode

4. Alpine's shell is located in the `/bin/sh` folder:

    * You'll need to compact the script into a one-liner using the `-c` flag and using semicolons to denote line breaks

    * The command you'll hand to the alpine image will look like this:

```zsh
/bin/sh -c "while :; do wget -qO- https://swapi.co/api/people/?search=r2; printf '\n'; sleep 5s; done"
```

Once you've successfully run your container it'll be happily chugging along in the background. But, in the background you won't be able to see the output of that container. 

```zsh
docker container run -d --name characters alpine:3.7.3 /bin/sh -c "while :; do
wget -qO- https://swapi.co/api/people/?search=r2; printf '\n'; sleep 5s; done"
```

Let's utilize the `docker container logs <containernameORcontainerID>`. This command will allow you to see what that container is running. Check your logs a few more times and you'll see your script doing it's thing

Nice! Let's make sure we clean up by using `docker container stop` and `docker container rm` to remove the character information generating container

## Solution

Run our named, detached container off alpine with our script!

```zsh
docker container run -d --name characters alpine:3.7.3 /bin/sh -c "while :; do
wget -qO- https://swapi.co/api/people/?search=r2; printf '\n'; sleep 5s; done"
```
