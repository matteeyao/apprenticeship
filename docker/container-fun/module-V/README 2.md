# Persistent Data in Docker

As stated before, Docker containers are not supposed to maintain any state. But what if we need state? In fact, some processes are inherently stateful, like a database. For example, a database needs to maintain all the data stored inside of it. If we store a database and its data inside a container, when it's `rm`, so  is the data. We are facing the problem of not having persistent data. Docker has a solution ready

## Bind Mounts

Bind mounts allow you to take a directory or individual file that exists on your machine (from herein called the Host, or Docker Host) and access that directory or file inside the running container. Any changes you make to the directory contents, or individual file will cause the file(s) to change on the Host machine

Start by running a fun named, detached, container based off the `nginx` image

```zsh
$ docker container run -d --name DogsRGood nginx
```

Now, while that's running that container in the background, let's enter the shell for nginx by utilizing the `-it` flag and the `exec` command you learned earlier along w/ the `bash` command. Now that you are in your container do a quick `ls` and look around your file system

Exit out of the container and make a new directory on your local computer. Put at least one file in the directory you created, and add some text to that file. We made a directory named `rad` and a simple text file within it:

```zsh
$ mkdir rad
$ touch rad/randomrad.txt
$ echo "hello world" >> rad/randomrad.txt
$ cat rad/randomrad.txt
hello world
```

Now let's mount this directory inside Docker, use a detached container w/ the `nginx` image, and look into the [bind mount](https://docs.docker.com/storage/bind-mounts/) docs to see examples of how to format your command. Use the `--mount` command for explicitness. You'll probably have a long command to enter, and w/ docker you can do multi-line commands like so:

```zsh
docker container run \
    multiple lines can be done with slashes \
    just like \
    this
```

Now go bind mount that volume (use the `--mount` command w/ a `type`, `source`, and `target`). Make sure your target path is **absolute** from your root.

```zsh
docker run -d --name nginx\
 --mount type=bind, source=/Users/rosekoron/rad,target=/rad \
 nginx
```

Now to test that it worked you can enter the new detached container you created using the `exec` command, the `-it` flag, and then hand it the command of `bash` to be executed:

```zsh
root@d46d99c3a840:/# ls
bin  boot  dev	etc  home  lib	lib64  media  mnt  opt	proc  rad  root  run  sbin  srv  sys  tmp  usr	var
root@d46d99c3a840:/# cd rad
root@d46d99c3a840:/rad# ls
randomrad.txt
root@d46d99c3a840:/rad# cat randomrad.txt
hello world
```

Now let's change the file in the container, and then exit the container:

```zsh
root@d46d99c3a840:/rad# echo "hello localhost" >> randomrad.txt
root@d46d99c3a840:/rad# exit
```

What happens when you look at that file on your localhost? It's changed! Same thing happens if we remove the file:

```zsh
root@d46d99c3a840:/rad# rm rad/randomrad.txt
root@d46d99c3a840:/rad# exit
~ ls -a rad
.  ..
```

So bind mounts can be helpful in local development if you are constantly changing a file - but as we just saw they are way more security prone b/c any change on your localhost will affect the data in your container! This is why you'll usually want to be using a `docker volume`

## Docker Volumes

Volumes have several advantages over bind mounts, here is a quick list as a reminder before you get started working w/ them:

1. Volumes are easier to back up or migrate than bind mounts.

2. You can manage volumes using Docker CLI commands or the Docker API.

3. Volumes work on both Linux and Windows containers.

4. Volumes can be more safely shared among multiple containers.

5. Volume  drivers allow you to store volumes on remote hosts or cloud providers, to encrypt the contents of volumes, or to add other functionality.

6. A new volume's contents can be pre-populated by a container.

As we've gone over before - you are never supposed to change a container, just deploy and redeploy. For this next part of the project, we'll be emulating a real life situation. What if you were working on a project w/ a Postgres database and a newer patch for that image came out w/ a security fix. You definitely want that new patch - but you also don't want to lose all your database data. So we'll utilize named volumes to carry the data from one container to another one

In short, we'll be updating a container from one version of Postgres to a newer patched version while maintaining the data in the database. Visit the Postgres image on [Docker Hub](https://hub.docker.com/_/postgres) and find any of the `9.6` versions of the image (**Hint**: you may have to look under the Tags tab). There you'll find the `Dockerfile` for this image but what you are interested is the `VOLUME` command in the `Dockerfile`. The `VOLUME` command will tell you where this image keeps its data - and we'll utilize that path in order to save that data

Create a detached container running the `9.6.1` version of the Postgres image, w/ a [named volume](https://success.docker.com/article/different-types-of-volumes) called `psql-data` pointing at the data inside the Postgres volume.

```zsh
docker container run -d --name postgres1 -v psql-data:/var/lib/postgresql/data
postgres:9.6.1
```

Check the logs for this container and make sure the database has finished starting up:

```zsh
docker container logs postgres1
```

You should see this message:

```zsh
PostgreSQL init process complete; ready for start up.
```

Make sure a volume w/ the name you specified was created by using `docker volume ls`. Inspect the volume you created `docker volume inspect psql-data`. Now we'll put some data into the volume as a test to make sure it'll properly transfer between the two containers. Enter the Postgres container w/ the following command: `docker container exec -it <YOUCONTAINERID> psql -U postgres`. Then once you are in postgres, post the following in order to create a table:

```sql
CREATE TABLE cats
(
id SERIAL PRIMARY KEY,
name VARCHAR (255) NOT NULL
);

-- cat seeding
INSERT INTO
cats (name)
VALUES
('Jet');
```

Test that the table worked correctly w/ a simple query to select all the information from the `cats` table. Awesome, now exit the container w/ `\q`, and then stop the container.

```zsh
docker stop postgres1
```

Look at your volumes again using `docker volume ls`. Your `psql-data` volume is still there even when your container stopped

Now create a new detached container w/ **the same named volume as before**, and a newer version of Postgres - `9.6.2`. Here's the final test! Check inside your new container using `docker container exec -it <YOURCONTAINERID> psql -U postgres`. Is the table you created earlier still there? If yes, then success. You upgraded a container while persisting the data

> [!NOTE]
> This will only work for patch versions, most SQL databases require manual commands to upgrade to major versions, meaning it's a DB limitation not a container one

You've worked with different containers and their networks, persisted data through bind mounts and volumes, and gotten a lot more comfortable with container commands. Make sure you clean up any remaining containers by using the `docker container rm -f` command, as well as getting rid of any unused volumes w/ `docker volume prune`


## Solution

### Bind Mounts

After you've created your folder:

```zsh
docker run -d --name nginx\
 --mount type=bind, source=/Users/rosekoron/rad,target=/rad \
 nginx
```
### Volumes

We create our first image:

```zsh
docker container run -d --name postgres1 -v psql-data:/var/lib/postgresql/data
postgres:9.6.1
```

Check Our Logs and Stop the First Container: 

```zsh
docker container logs postgres1
```

Then Enter the container to make our table: 

```zsh
docker container exec -it postgres1 psql -U postgres
```

Then Stop Our Container: 

```zsh
docker stop postgres1
```

Run the second container withe the same volume:

```zsh
docker container run -d --name postgres2 -v psql-data:/var/lib/postgresql/data
postgres:9.6.2
```

Then Enter Our Second Container: 

```zsh
docker container exec -it postgres2 psql -U postgres
```
