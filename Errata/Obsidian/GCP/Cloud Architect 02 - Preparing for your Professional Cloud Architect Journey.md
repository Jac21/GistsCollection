# Introduction to the Professional Cloud Architect Certification

## Diagnostic Questions
check_

1.

Cymbal Direct developers have written a new application. Based on initial usage estimates, you decide to run the application on Compute Engine instances with 15 Gb of RAM and 4 CPUs. These instances store persistent data locally. After the application runs for several months, historical data indicates that the application requires 30 Gb of RAM. Cymbal Direct management wants you to make adjustments that will minimize costs. What should you do?

Stop the instance, and then use the command gcloud compute instances set-machine-type VM_NAME --machine-type 2-custom-4-30720. Set the instance’s metadata to: preemptible: true. Start the instance again.

Stop the instance, and then use the command gcloud compute instances set-machine-type VM_NAME --machine-type e2-standard-8. Start the instance again.

Stop the instance, and then use the command gcloud compute instances set-machine-type VM_NAME --machine-type e2-standard-8. Set the instance’s metadata to: preemptible: true. Start the instance again.

_check_Stop the instance, and then use the command gcloud compute instances set-machine-type VM_NAME --machine-type 2-custom-4-30720. Start the instance again.

Correct! Custom instances are a good way to optimize costs. You don’t have to pay for resources you don’t need.

_check_

2.

Cymbal Direct is evaluating database options to store the analytics data from its experimental drone deliveries. You're currently using a small cluster of MongoDB NoSQL database servers. You want to move to a managed NoSQL database service with consistent low latency that can scale throughput seamlessly and can handle the petabytes of data you expect after expanding to additional markets. What should you do?

Extract the data from MongoDB, and insert the data into BigQuery.

Extract the data from MongoDB. Insert the data into Firestore using Datastore mode.

Extract the data from MongoDB. Insert the data into Firestore using Native mode.

_check_Create a Bigtable instance, extract the data from MongoDB, and insert the data into Bigtable.

Correct! Bigtable is ideal for IoT, gives consistently sub-10ms latency, and can be used at a petabyte scale.

_check_

3.

You are working in a mixed environment of VMs and Kubernetes. Some of your resources are on-premises, and some are in Google Cloud. Using containers as a part of your CI/CD pipeline has sped up releases significantly. You want to start migrating some of those VMs to containers so you can get similar benefits. You want to automate the migration process where possible. What should you do?

Use Migrate for Compute Engine to import VMs and convert them to containers.

_check_Manually create a GKE cluster, and then use Migrate for Anthos to set up the cluster, import VMs, and convert them to containers.

Manually create a GKE cluster. Use Cloud Build to import VMs and convert them to containers.

Use Migrate for Anthos to automate the creation of Compute Engine instances to import VMs and convert them to containers.

Correct. You must initially create a GKE cluster. Then you can use Migrate for Anthos to set up the cluster and import the VMs.

_check_

4.

You are working with a client who is using Google Kubernetes Engine (GKE) to migrate applications from a virtual machine–based environment to a microservices-based architecture. Your client has a complex legacy application that stores a significant amount of data on the file system of its VM. You do not want to re-write the application to use an external service to store the file system data. What should you do?

In Cloud Shell, create a YAML file defining your Deployment called deployment.yaml. Create a Deployment in GKE by running the command kubectl apply -f deployment.yaml

In Cloud Shell, create a YAML file defining your Container called build.yaml. Create a Container in GKE by running the command gcloud builds submit –config build.yaml .

_check_In Cloud Shell, create a YAML file defining your StatefulSet called statefulset.yaml. Create a StatefulSet in GKE by running the command kubectl apply -f statefulset.yaml

In Cloud Shell, create a YAML file defining your Pod called pod.yaml. Create a Pod in GKE by running the command kubectl apply -f pod.yaml

Correct! A StatefulSet represents a group of persistent Pods. The YAML file will define a PersistentVolumeClaim (PVC) that allows for an application to retain state. A StatefulSet is commonly used with applications like databases.

_check_

5.

You are creating a new project. You plan to set up a Dedicated interconnect between two of your data centers in the near future and want to ensure that your resources are only deployed to the same regions where your data centers are located. You need to make sure that you don’t have any overlapping IP addresses that could cause conflicts when you set up the interconnect. You want to use RFC 1918 class B address space. What should you do?

Create a new project, delete the default VPC network, set up the network in custom mode, and then use IP addresses in the 192.168.x.x address range to create subnets in your desired zones. Use VPC Network Peering to connect the zones in the same region to create regional networks.

_check_Create a new project, delete the default VPC network, set up a custom mode VPC network, and then use IP addresses in the 172.16.x.x address range to create subnets in your desired regions.

Create a new project, leave the default network in place, and then use the default 10.x.x.x network range to create subnets in your desired regions.

Create a new project, delete the default VPC network, set up an auto mode VPC network, and then use the default 10.x.x.x network range to create subnets in your desired regions.

Correct! Custom networks give you full control.

_check_

6.

Customers need to have a good experience when accessing your web application so they will continue to use your service. You want to define key performance indicators (KPIs) to establish a service level objective (SLO). Which KPI could you use?

Eighty-five percent of customers are satisfied users

_check_Eighty-five percent of requests succeed when aggregated over 1 minute

Eighty-five percent of requests are successful

Low latency for > 85% of requests when aggregated over 1 minute

Correct! This is specific, and you can reasonably expect to meet this KPI.

_check_

7.

Cymbal Direct drones continuously send data during deliveries. You need to process and analyze the incoming telemetry data. After processing, the data should be retained, but it will only be accessed once every month or two. Your CIO has issued a directive to incorporate managed services wherever possible. You want a cost-effective solution to process the incoming streams of data. What should you do?

Ingest data with ClearBlade IoT Core, and then publish to Pub/Sub. Use BigQuery to process the data, and store it in a Standard Cloud Storage bucket.

Ingest data with ClearBlade IoT Core, process it with Dataprep, and store it in a Coldline Cloud Storage bucket.

_check_Ingest data with ClearBlade IoT Core, and then publish to Pub/Sub. Use Dataflow to process the data, and store it in a Nearline Cloud Storage bucket.

Ingest data with ClearBlade IoT Core, and then store it in BigQuery.

Correct! Dataflow is a fully managed service that can be used to process both streams and batches of data. Nearline is a good fit because the data could be accessed every month.

_close_

8.

Cymbal Direct's employees will use Google Workspace. Your current on-premises network cannot meet the requirements to connect to Google's public infrastructure. What should you do?

_close_Order a Partner Interconnect from a Google Cloud partner, and ensure that proper routes are configured.

Order a Dedicated Interconnect from a Google Cloud partner, and ensure that proper routes are configured.

Connect the network to a Google point of presence, and enable Direct Peering.

Connect the on-premises network to Google’s public infrastructure via a partner that supports Carrier Peering.

Incorrect. A Partner Interconnect allows access to your VPC network via a partner.This would allow access between on-premises and your internal IP addresses, which greatly exceeds the scope and does not follow the principle of least privilege.

_check_

9.

Cymbal Direct has created a proof of concept for a social integration service that highlights images of its products from social media. The proof of concept is a monolithic application running on a single SuSE Linux virtual machine (VM). The current version requires increasing the VM’s CPU and RAM in order to scale. You would like to refactor the VM so that you can scale out instead of scaling up. What should you do?

_check_Use containers instead of VMs, and use a GKE autoscaling deployment.

Move the existing codebase and VM provisioning scripts to git, and attach external persistent volumes to the VMs.

Make sure that the application declares any dependent requirements in a requirements.txt or equivalent statement so that they can be referenced in a startup script. Specify the startup script in a managed instance group template, and use an autoscaling policy.

Make sure that the application declares any dependent requirements in a requirements.txt or equivalent statement so that they can be referenced in a startup script, and attach external persistent volumes to the VMs.

Correct! Treating each app as one or more stateless processes means externalizing state to a separate database service. This allows for more concurrent processing.

_check_

10.

Cymbal Direct is working with Cymbal Retail, a separate, autonomous division of Cymbal with different staff, networking teams, and data center. Cymbal Direct and Cymbal Retail are not in the same Google Cloud organization. Cymbal Retail needs access to Cymbal Direct’s web application for making bulk orders, but the application will not be available on the public internet. You want to ensure that Cymbal Retail has access to your application with low latency. You also want to avoid egress network charges if possible. What should you do?

_check_Verify that the subnet range Cymbal Retail is using doesn’t overlap with Cymbal Direct’s subnet range, and then enable VPC Network Peering for the project.

If Cymbal Retail does not have access to a Google Cloud data center, use Carrier Peering to connect the two networks.

Verify that the subnet Cymbal Retail is using has the same IP address range with Cymbal Direct’s subnet range, and then enable VPC Network Peering for the project.

Specify Cymbal Direct’s project as the Shared VPC host project, and then configure Cymbal Retail’s project as a service project.

Correct! VPC Peering allows for shared networking between organizations.

## Knowledge Check

check_

1.

If you have a business requirement to minimize costs, what are two things you could do?

Cap costs by creating a budget in Google Cloud

Use a managed service

Migrate to Kubernetes from VMs

_check_Follow Google’s rightsizing recommendations

Correct. Google will evaluate your instance’s usage, and make recommendations on how to save money or improve performance by resizing them.

_check_Do not run instances when they are not being used

Correct. Only running, and thus paying for, instances when they are needed, is a great way to save costs. Using tools like GKE autoscaling clusters, or managed instance groups are a great way to save money.

_check_

2.

What could Cymbal Direct use to estimate costs for their Google Cloud environment?

_check_Cloud Pricing Calculator

ROI

Average Compute Instance CPU

KPIs

Correct! The Cloud pricing calculator allows you to estimate the costs for Google Cloud Products and Services.

# Managing and Provisioning a Solution Infrustructure

## Diagnostic Questions

_check_

1.

Your existing application runs on Ubuntu Linux VMs in an on-premises hypervisor. You want to deploy the application to Google Cloud with minimal refactoring. What should you do?

_check_Write Terraform scripts to deploy the application as Compute Engine instances.

Isolate the core features that the application provides. Use App Engine to deploy each feature independently as a microservice.

Set up a Google Kubernetes Engine (GKE) cluster, and then create a deployment with an autoscaler.

Use a Dedicated or Partner Interconnect to connect the on-premises network where your application is running to your VPC: Configure an endpoint for a global external HTTP(S) load balancer that connects to the existing VMs.

Correct! Terraform lets you manage how you deploy and manage a variety of services in Google Cloud, such as Compute Engine. You can also use Cloud Deployment Manager for this purpose.

_check_

2.

Cymbal Direct's user account management app allows users to delete their accounts whenever they like. Cymbal Direct also has a very generous 60-day return policy for users. The customer service team wants to make sure that they can still refund or replace items for a customer even if the customer’s account has been deleted. What can you do to ensure that the customer service team has access to relevant account information?

Disable the account. Export account information to Cloud Storage. Have the customer service team permanently delete the data after 30 days.

Ensure that the user clearly understands that after they delete their account, all their information will also be deleted. Remind them to download a copy of their order history and account information before deleting their account. Have the support agent copy any open or recent orders to a shared spreadsheet.

_check_Temporarily disable the account for 30 days. Export account information to Cloud Storage, and enable lifecycle management to delete the data in 60 days.

Restore a previous copy of the user information database from a snapshot. Have a database administrator capture needed information about the customer.

Correct! This takes a lazy deletion approach and allows support or administrators to restore data later if necessary.

_check_

3.

You have deployed your frontend web application in Kubernetes. Based on historical use, you need three pods to handle normal demand. Occasionally your load will roughly double. A load balancer is already in place. How could you configure your environment to efficiently meet that demand?

Use the "kubectl autoscale" command to change the pod's maximum number of instances to six.

Edit your deployment's configuration file and change the number of replicas to six.

Edit your pod's configuration file and change the number of replicas to six.

_check_Use the "kubectl autoscale" command to change the deployment’s maximum number of instances to six.

Correct! This will allow Kubernetes to scale the number of pods automatically, based on a condition like CPU load or requests per second.

_check_

4.

Cymbal Direct needs to use a tool to deploy its infrastructure. You want something that allows for repeatable deployment processes, uses a declarative language, and allows parallel deployment. You also want to deploy infrastructure as code on Google Cloud and other cloud providers. What should you do?

Use Google Kubernetes Engine (GKE) to create deployments and manifests for your applications.

Automate the deployment with Cloud Deployment Manager.

Develop in Docker containers for portability and ease of deployment.

_check_Automate the deployment with Terraform scripts.

Correct! Terraform lets you automate and manage resources in multiple clouds.

_close_

5.

You need to deploy a load balancer for a web-based application with multiple backends in different regions. You want to direct traffic to the backend closest to the end user, but also to different backends based on the URL the user is accessing. Which of the following could be used to implement this?

The request is matched by a URL map and then sent to a SSL proxy load balancer. A global forwarding rule sends the request to a target proxy, which selects a backend service and sends the request to Compute Engine instance groups in multiple regions.

The request is received by the SSL proxy load balancer, which uses a global forwarding rule to check the URL map, then sends the request to a backend service. The request is processed by Compute Engine instance groups in multiple regions.

_close_The request is matched by a URL map and then sent to a global external HTTP(S) load balancer. A global forwarding rule sends the request to a target proxy, which selects a backend service. The backend service sends the request to Compute Engine instance groups in multiple regions.

The request is received by the global external HTTP(S) load balancer. A global forwarding rule sends the request to a target proxy, which checks the URL map and selects the backend service. The backend service sends the request to Compute Engine instance groups in multiple regions.

Incorrect. The external global HTTP(S) load balancer must exist to provide the multicast IP address, and then route the request through the target proxy.

_check_

6.

Cymbal Direct must meet compliance requirements. You need to ensure that employees with valid accounts cannot access their VPC network from locations outside of its secure corporate network, including from home. You also want a high degree of visibility into network traffic for auditing and forensics purposes. What should you do?

_check_Enable VPC Service Controls, define a network perimeter to restrict access to authorized networks, and enable VPC Flow Logs for the networks you need to monitor.

Ensure that all users install Cloud VPN. Enable VPC Flow Logs for the networks you need to monitor.

Enable Identity-Aware Proxy (IAP) to allow users to access services securely. Use Google Cloud’s operations suite to view audit logs for the networks you need to monitor.

Enable VPC Service Controls, and use Google Cloud’s operations suite to view audit logs for the networks you need to monitor.

Correct! Enabling VPC Service Controls lets you define a network perimeter. VPC Flow Logs lets you log network-level communication to Compute Engine instances.

_check_

7.

Cymbal Direct wants to create a pipeline to automate the building of new application releases. What sequence of steps should you use?

Run unit tests. Deploy. Build a Docker container. Check in code. Set up a source code repository.

_check_Set up a source code repository. Check in code. Run unit tests. Build a Docker container. Deploy.

Check in code. Set up a source code repository. Run unit tests. Deploy. Build a Docker container.

Set up a source code repository. Run unit tests. Check in code. Deploy. Build a Docker container.

Correct! Each step is dependent on the previous step. These are in the right order.

_check_

8.

Cymbal Direct wants to allow partners to make orders programmatically, without having to speak on the phone with an agent. What should you consider when designing the API?

The API backend should be tightly coupled. Clients should know a significant amount about the services they use. REST APIs using gRPC should be used for all external APIs.

The API backend should be loosely coupled. Clients should not be required to know too many details of the services they use. REST APIs using gRPC should be used for all external APIs.

_check_The API backend should be loosely coupled. Clients should not be required to know too many details of the services they use. For REST APIs, HTTP(S) is the most common protocol.

The API backend should be tightly coupled. Clients should know a significant amount about the services they use. For REST APIs, HTTP(S) is the most common protocol used.

Correct! Loose coupling has several benefits, including maintainability, versioning, and reduced complexity. Clients not knowing the backend systems means that these systems can be more easily replaced or modified, and HTTP(S) is the most common protocol used for external REST APIs.

_check_

9.

Cymbal Direct wants a layered approach to security when setting up Compute Engine instances. What are some options you could use to make your Compute Engine instances more secure?

Use labels to allow traffic only from certain sources and ports. Use a Compute Engine service account.

Use labels to allow traffic only from certain sources and ports. Turn on Secure boot and vTPM.

Use network tags to allow traffic only from certain sources and ports. Use a Compute Engine service account.

_check_Use network tags to allow traffic only from certain sources and ports. Turn on Secure boot and vTPM.

Correct! You can use network tags with firewall rules to automatically associate instances when they are created. Secure boot and vTPM protect the OS from being compromised.

_check_

10.

You are working with a client who has built a secure messaging application. The application is open source and consists of two components. The first component is a web app, written in Go, which is used to register an account and authorize the user’s IP address. The second is an encrypted chat protocol that uses TCP to talk to the backend chat servers running Debian. If the client's IP address doesn't match the registered IP address, the application is designed to terminate their session. The number of clients using the service varies greatly based on time of day, and the client wants to be able to easily scale as needed. What should you do?

Deploy the web application using the App Engine standard environment with a global external HTTP(S) load balancer and a network endpoint group. Use a managed instance group for the backend chat servers. Use a global SSL proxy load balancer to load-balance traffic across the backend chat servers.

_check_Deploy the web application using the App Engine standard environment with a global external HTTP(S) load balancer and a network endpoint group. Use a managed instance group for the backend chat servers. Use an external network load balancer to load-balance traffic across the backend chat servers.

Deploy the web application using the App Engine flexible environment with a global external HTTP(S) load balancer and a network endpoint group. Use an unmanaged instance group for the backend chat servers. Use an external network load balancer to load-balance traffic across the backend chat servers.

Deploy the web application using the App Engine standard environment with a global external HTTP(S) load balancer and a network endpoint group. Use an unmanaged instance group for the backend chat servers. Use an external network load balancer to load-balance traffic across the backend chat servers.

Correct! Using App Engine allows for dynamic scaling based on demand, as does a managed instance group. Using an external network load balancer preserves the client's IP address.

## Knowledge Check

_check_

1.

Which network configuration would ensure low latency for US drone pilots?

Only deploy resources to Regions in Asia

Only deploy resources to Regions in Europe

_check_Only deploy resources to Regions in the US

Deploy resources globally

Correct! Only deploy resources to Regions in the US

_check_

2.

Which Storage Class should you use for data that is going to be accessed at least once every two weeks?

_check_Standard

Nearline

Coldline

Archive

Correct! Standard is appropriate for frequent use.

# Designing for Security and Compliance

## Diagnostic Questions

check_

1.

Cymbal Direct is experiencing success using Google Cloud and you want to leverage tools to make your solutions more efficient. Erik, one of the original web developers, currently adds new products to your application manually. Erik has many responsibilities and requires a long lead time to add new products. You need to create an App Engine application to let Cymbal Direct employees add new products instead of waiting for Erik. However, you want to make sure that only authorized employees can use the application. What should you do?

Use Google Cloud Armor to restrict access to the corporate network's external IP address. Configure firewall rules to allow only HTTP(S) access.

_check_Create a Google group and add authorized employees to it. Configure Identity-Aware Proxy (IAP) to the App Engine application as a HTTP-resource. Add the group as a principle with the role "IAP-secured Web App User."

Create a Google group and add authorized employees to it. Configure Identity-Aware Proxy (IAP) to the App Engine application as a HTTP-resource. Add the group as a principle with the role "Project Owner."

Set up Cloud VPN between the corporate network and the Google Cloud project's VPC network. Allow users to connect to the App Engine instance.

Correct! You could use individual accounts to give out access instead of a group, and by doing so you make access more manageable. Identity-Aware Proxy is a great tool for exactly this kind of issue.

_check_

2.

Michael is the owner/operator of “Zneeks,” a retail shoe store that caters to sneaker aficionados. He regularly works with customers who order small batches of custom shoes. Michael is interested in using Cymbal Direct to manufacture and ship custom batches of shoes to these customers. Reasonably tech-savvy but not a developer, Michael likes using Cymbal Direct's partner purchase portal but wants the process to be easy. What is an example of a user story that could describe Michael’s persona?

Michael is a tech-savvy owner/operator of a small business.

Zneeks is a retail shoe store that caters to sneaker aficionados.

Michael is reasonably tech-savvy but needs Cymbal Direct's partner purchase portal to be easy

_check_As a shoe retailer, Michael wants to send Cymbal Direct custom purchase orders so that batches of custom shoes are sent to his customers.

Correct! “As a [type of user], I want to [do something] so that I can [get some benefit]” is the standard format for a user story.

_check_

3.

Cymbal Direct wants to use Identity and Access Management (IAM) to allow employees to have access to Google Cloud resources and services based on their job roles. Several employees are project managers and want to have some level of access to see what has been deployed. The security team wants to ensure that securing the environment and managing resources is simple so that it will scale. What approach should you use?

Grant access by assigning predefined roles to groups. Use multiple groups for better control. Make sure you give out access to all the children in a hierarchy under the level needed, because child resources will not automatically inherit abilities.

_check_Grant access by assigning predefined roles to groups. Use multiple groups for better control. Give access as low in the hierarchy as possible to prevent the inheritance of too many abilities from a higher level.

Give access directly to each individual for more granular control. Give access as low in the hierarchy as possible to prevent the inheritance of too many abilities from a higher level.

Grant access by assigning custom roles to groups. Use multiple groups for better control. Give access as low in the hierarchy as possible to prevent the inheritance of too many abilities from a higher level.

Correct! This follows recommended practices regarding organizational policies.

_check_

4.

Cymbal Direct’s social media app must run in a separate project from its APIs and web store. You want to use Identity and Access Management (IAM) to ensure a secure environment. How should you set up IAM?

Use one service account for each component (social media app, APIs, and web store) with predefined or custom roles to grant access.

Use one service account for each component (social media app, APIs, and web store) with basic roles to grant access.

_check_Use separate service accounts for each component (social media app, APIs, and web store) with predefined or custom roles to grant access.

Use separate service accounts for each component (social media app, APIs, and web store) with basic roles to grant access.

Correct! Using separate service accounts for each component allows you to grant only the access needed to each service account with either a predefined or custom role.

_check_

5.

Cymbal Direct needs to make sure its new social media integration service can’t be accessed directly from the public internet. You want to allow access only through the web frontend store. How can you prevent access to the social media integration service from the outside world, but still allow access to the APIs of social media services?

Limit access to the external IP addresses of the VM instances using firewall rules and place them in a private VPC behind Cloud NAT. Any SSH connection for management should be done with Identity-Aware Proxy (IAP) or a bastion host (jump box) after allowing SSH access from IAP or a corporate network.

Remove external IP addresses from the VM instances running the social media service and place them in a private VPC behind Cloud NAT. Any SSH connection for management should be restricted to corporate network IP addresses by Google Cloud Armor.

Limit access to the external IP addresses of the VM instances using a firewall rule to block all outbound traffic. Any SSH connection for management should be done with Identity-Aware Proxy (IAP) or a bastion host (jump box) after allowing SSH access from IAP or a corporate network.

_check_Remove external IP addresses from the VM instances running the social media service and place them in a private VPC behind Cloud NAT. Any SSH connection for management should be done with Identity-Aware Proxy (IAP) or a bastion host (jump box) after allowing SSH access from IAP or a corporate network.

Correct! Using Cloud NAT will prevent inbound access from the outside world but will allow connecting to social media APIs outside of the VPC. Using IAP or a bastion host allows for management by SSH, but without the complexity of using VPNs for user access.

_check_

6.

You have several Compute Engine instances running NGINX and Tomcat for a web application. In your web server logs, many login failures come from a single IP address, which looks like a brute force attack. How can you block this traffic?

Ensure that an HTTP(S) load balancer is configured to send traffic to your backend Compute Engine instances running your web server. Create a Google Cloud Armor policy using the instance’s local firewall with a default rule action of "Allow." Add a new local firewall rule that specifies the IP address causing the login failures as the Condition, with an action of "Deny" and a deny status of "403," and accept the default priority (1000).

Ensure that an HTTP(S) load balancer is configured to send traffic to the backend Compute Engine instances running your web server. Create a Google Cloud Armor policy with a default rule action of "Deny." Add a new rule that specifies the IP address causing the login failures as the Condition, with an action of "Deny" and a deny status of "403," and accept the default priority (1000). Add the load balancer backend service's HTTP-backend as the target.

Edit the Compute Engine instances running your web application, and enable Google Cloud Armor. Create a Google Cloud Armor policy with a default rule action of "Allow." Add a new rule that specifies the IP address causing the login failures as the Condition, with an action of "Deny” and a deny status of "403," and accept the default priority (1000).

_check_Ensure that an HTTP(S) load balancer is configured to send traffic to the backend Compute Engine instances running your web server. Create a Google Cloud Armor policy with a default rule action of "Allow." Add a new rule that specifies the IP address causing the login failures as the Condition, with an action of "Deny" and a deny status of "403," and accept the default priority (1000). Add the load balancer backend service's HTTP-backend as the target.

Correct! Configuring a Google Cloud Armor rule to prevent that IP address from accessing the HTTP-backend on the load balancer will prevent access.

_check_

7.

Cymbal Direct has an application running on a Compute Engine instance. You need to give the application access to several Google Cloud services. You do not want to keep any credentials on the VM instance itself. What should you do?

Create a service account and assign it the project owner role, which enables access to any needed service.

Create a service account for the instance. Use Access scopes to enable access to the required services.

Create a service account for each of the services the VM needs to access. Associate the service accounts with the Compute Engine instance.

_check_Create a service account with one or more predefined or custom roles, which give access to the required services.

Correct! This gives the flexibility and granularity needed to allow access to multiple services, without giving access to unnecessary services.

_check_

8.

Your client is legally required to comply with the Payment Card Industry Data Security Standard (PCI-DSS). The client has formal audits already, but the audits are only done periodically. The client needs to monitor for common violations to meet those requirements more easily. The client does not want to replace audits but wants to engage in continuous compliance and catch violations early. What would you recommend that this client do? Responses:

Enable the Security Command Center (SCC) dashboard, asset discovery, and Security Health Analytics in the Standard tier. Export or view the PCI-DSS Report from the SCC dashboard's Compliance tab.

Enable the Security Command Center (SCC) dashboard, asset discovery, and Security Health Analytics in the Standard tier. Export or view the PCI-DSS Report from the SCC dashboard's Vulnerabilities tab.

Enable the Security Command Center (SCC) dashboard, asset discovery, and Security Health Analytics in the Premium tier. Export or view the PCI-DSS Report from the SCC dashboard's Vulnerabilities tab.

_check_Enable the Security Command Center (SCC) dashboard, asset discovery, and Security Health Analytics in the Premium tier. Export or view the PCI-DSS Report from the SCC dashboard's Compliance tab.

Correct! The reports relating to compliance vulnerabilities are on the Compliance tab. To use the Security Health Analytics that scan for common compliance vulnerabilities, you must use the Premium tier.

_check_

9.

You've recently created an internal App Engine application for developers in your organization. The application lets developers clone production Cloud SQL databases into a project specifically created to test code and deployments. Your previous process was to export a database to a Cloud Storage bucket, and then import the SQL dump into a legacy on-premises testing environment database with connectivity to Google Cloud via Cloud VPN. Management wants to incentivize using the new process with Cloud SQL for rapid testing and track how frequently rapid testing occurs. How can you ensure that the developers use the new process?

Use an ACL on the Cloud Storage bucket. Create a read-only group that only has viewer privileges, and ensure that the developers are in that group.

Use predefined roles to restrict access to what the developers are allowed to do. Create a group for the developers, and associate the group with the Cloud SQL Viewer role. Remove the "cloudsql.instances.export" ability from the role.

_check_Create a custom role to restrict access to what developers are allowed to do. Create a group for the developers, and associate the group with your custom role. Ensure that the custom role does not have "cloudsql.instances.export."

Leave the ACLs on the Cloud Storage bucket as-is. Disable Cloud VPN, and have developers use Identity-Aware Proxy (IAP) to connect. Create an organization policy to enforce public access protection.

Correct! In this scenario, using a predefined role is inappropriate because the most appropriate predefined role, Cloud SQL Viewer, contains the cloudsql.instances.export capability, which would allow the database to be exported.

_check_

10.

Your client created an Identity and Access Management (IAM) resource hierarchy with Google Cloud when the company was a startup. Your client has grown and now has multiple departments and teams. You want to recommend a resource hierarchy that follows Google-recommended practices. What should you do?

Keep all resources in one project, but change the resource hierarchy to reflect company organization.

Keep all resources in one project, and use a flat resource hierarchy to reduce complexity and simplify management.

Use a flat resource hierarchy and multiple projects with established trust boundaries.

_check_Use multiple projects with established trust boundaries, and change the resource hierarchy to reflect company organization.

Correct! Because the environment has evolved, update the IAM resource hierarchy to reflect the changes. Use projects to group resources that share the same trust boundary.

## Knowledge Check

check_

1.

Cymbal Direct has chosen to use multiple projects for their environment. How do you describe this choice?

Using multiple projects requires creating separate IAM policies at each project level

Unnecessary. Using multiple projects adds little to no benefits.

Using multiple projects only adds security benefits.

_check_Using multiple projects adds both security and other benefits.

Correct - in all but the simplest environments, having multiple projects can be very beneficial. You get security benefits, but also can manage each project independently, with their own resources, policies, and billing.

_check_

2.

What type of data might be inadvertently picked up by a drone during a delivery?

Classified government data

_check_Video of private property

Healthcare data regulated by privacy laws

Financial data regulated by banking laws

Correct! Video of private property could include potential privacy violations depending on the jurisdiction and what the video is of. For example, the California Consumer Privacy Act could prohibit retaining photos of the customer as proof of delivery