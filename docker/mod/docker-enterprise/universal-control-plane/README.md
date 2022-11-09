# Setting up Universal Control Plane (UCP)

Docker **Universal Control Plane (UCP)** provides enterprise-level cluster management.

At first glance, UCP may look like "Docker Swarm w/ a web interface," but it provides additional features as well, such as:

* Organization and team management

* Role-based access control

* Orchestration w/ both Docker Swarm and Kubernetes

## Setting up Universal Control Plane (UCP)

**Universal Control Plane (UCP)**: An enterprise-level Docker cluster which provides a web UI that allows us to manage the Docker swarm. It also includes a Kubernetes cluster, role-based access control (RBAC), and other advanced features.

1. To install UCP, use the `ucp` image:

```zsh
docker container run --rm -it --name ucp \
    -v /var/run/docker.sock:/var/run/docker.sock \
    docker/ucp:3.1.5 install \
    --host-address $PRIVATE_IP \
    --interactive
```

2. When prompted, create some admin credentials.

> [!NOTE]
> 
> We will also be prompted for `Additional Aliases`. Once this happens, hit enter to accept the default value.

3. Once the installation is complete, access UCP in a web browser using the UCP manager's **Public** IP address: `https://<YOUR_UCP_MANAGER_PUBLIC_IP>`.

4. Log in to the UCP manager using the credentials we created earlier.

> [!NOTE]
> 
> We will be prompted to provide a license.

5. Open another tab and go to https://hub.docker.com/my-content.

6. Click **Setup**.

7. Download the license using the _license key_ link.

8. Go back to the UCP tab and click `Upload License`. Select the license file that we just downloaded and upload it.

9. In a browser, on the UCP dashboard, click **Shared Resources** ▶︎ **Nodes** ▶︎ **Add Node**

10. Make sure the _node type_ is `Linux` and the _node role_ is `Worker`. Then, copy the `docker swarm join`
    command that appears on the page.

11. Run the `docker swarm join` obtained from the UCP manager on all worker nodes.

12. If we go to **Shared Resources**, and then `Nodes` on the UCP dashboard in a browser, we should see both worker nodes appear in the list.

## Security in UCP

UCP provides a flexible model for controlling access to cluster resources and functionality. Essentially, UCP allows you to restrict access to various users of your Docker Enterprise infrastructure and only give them access to specifically what they need access to, whether that's specific services or specific containers, or anything that you can manage in UCP. You can give users very granular access to those resources.

**User**: a person who is authenticated

**Team**: a group of users that share certain permissions

**Service Account**: a Kubernetes object that allows a container to access cluster resources

**Subject**: a user, team, organization, or service account that has the ability to do something

**Resource set**: a Docker swarm collection of cluster objects, like containers, services, and nodes OR a Kubernetes Namespace, a logical subdivision of the Kubernetes cluster

**Role**: a permission that can be used to operate on objects in a collection

In summary, the **subject** is the person being given the permission. Our **resource set**, or collection, is the thing that they are going to be acting upon, and the **role** is the specific action that they are allowed to perform.

**Grant**: Provides a specific permission (role) to a subject, w/ regard to a resource set

UCP also support **LDAP integration**, allowing you to manage users and teams using an LDAP-enabled user directory.

## Learning summary

UCP implements its own role-based access control (RBAC) model with the following components:

* User: An authenticated person.

* Team: A group of users who share a set of permissions.

* Organization: A group of teams.

* Subject: A user, team, or organization.

* Collection: A set of objects in the swarm (containers, services, nodes etc.).

* Role: A specific permission that defines what an entity can do with regard to a collection of objects.

* Grant: Provides a specific permission (role) to a subject in regards to a collection.
