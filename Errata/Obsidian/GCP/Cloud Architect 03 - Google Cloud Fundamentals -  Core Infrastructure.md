# Introducing Google Cloud

## Quiz

check_

1.

What type of cloud computing service lets you bind your application code to libraries that give access to the infrastructure your applicationneeds?

_check_Platform as a service

Infrastructure as a service

Hybrid cloud

Software as a service

Virtualized data centers

Correct!

_check_

2.

What is the primary benefit to a Google Cloud customer of using resources in several zones within a region?

For better performance

For getting discounts on other zones

_check_For improved fault tolerance

For expanding services to customers in new areas

Correct!

_check_

3.

Why might a Google Cloud customer use resources in several regions around the world?

_check_To bring their applications closer to users around the world, and for improved fault tolerance

To improve security

To earn discounts

To offer localized application versions in different regions.

That is correct.

# Resources and Access in the Cloud

## Quiz

_check_

1.

Order these IAM role types from broadest to finest-grained.

Predefined roles, custom roles, basic roles

Custom roles, predefined roles, basic roles

_check_Basic roles, predefined roles, custom roles

Correct!

_check_

2.

Which of these values is globally unique, permanent, and unchangeable, but chosen by the customer?

The project's billing credit-card number

_check_The project ID

The project name

The project number

Correct! The project ID is immutable (cannot be changed) after creation, but can be changed during creation.

_check_

3.

Choose the correct completion: Services and APIs are enabled on a per-__________ basis.

Billing account

Organization

_check_Project

Folder

Correct!

# Virtual Machines and Networks in the Cloud

## Quiz

_check_

1.

In Google Cloud VPCs, what scope do subnets have?

Zonal

Multi-regional

Global

_check_Regional

That's correct. VPC subnets can span the zones that make up a region. This is beneficial because your solutions can incorporate fault tolerance without complicating your network topology.

_check_

2.

What is the main reason customers choose Preemptible VMs?

To use custom machine types

_check_To reduce cost.

To improve performance.

To reduce cost on premium operating systems.

That's correct! The per-hour price of preemptible and spot VMs incorporates a substantial discount.

_check_

3.

For which of these interconnect options is a Service Level Agreement available?

Carrier Peering

Standard Network Tier

_check_Dedicated Interconnect

Direct Peering

Correct!

# Storage in the Cloud

## Quiz

_check_

1.

Which database service can scale to higher database sizes?

Cloud Bigtable

Firestore

Cloud SQL

_check_Cloud Spanner

Correct! Cloud Spanner can scale to petabyte database sizes, while Cloud SQL is limited by the size of the database instances you choose. At the time this quiz was created, the maximum was 10,230 GB.

_check_

2.

What is the correct use case for Cloud Storage?

_check_Cloud Storage is well suited to providing durable and highly available object storage.

Cloud Storage is well suited to providing RDBMS services.

Cloud Storage is well suited to providing the root file system of a Linux virtual machine.

Cloud Storage is well suited to providing data warehousing services.

That's correct! Cloud Storage is object storage rather than filestorage.

_check_

3.

You are building a small application. If possible, you'd like this application's data storage to be at no additional charge. Which service has a free daily quota, separate from any free trials?

Bigtable

Cloud Spanner

_check_Firestore

Cloud SQL

Correct!