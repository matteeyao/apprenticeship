# DNS: A Quick Summary & FAQ

Humans are better w/ names than they are w/ numbers. As such, it's easier to remember the domain name attached to the site (reclaimhosting.com) than the IP address of the server where the site lives (162.243.224.94)

B/c computers are better w/ numbers, not names, we use a system for translating domains to IP addresses and vice versa. This system is DNS, which stands for **Domain Name System**. Just as it sounds, DNS is a protocol for names of systems

## DNS Queries

When you type `reclaimhosting.com` into your web browser, you are asking a **resolver**, or query participant, to send out a DNS query. A DNS query can look like one of two things:

* A **recursive** query asks the DNS server, "Can you look for the IP for me and report back?"

* An **iterative** query says, "if you can't find it, send along the next place I should look. I'll keep looking until I find an answer"

Once the end DNS server receives the query, it sends a message back to your browser which tells your browser which IP address it has located. At that point, the translation is over and the browser communicates using just IP addresses from that point on

## DNS Records

DNS works by holding individual records. **Records** are simply single mappings between a domain location and a server. You can think of a single record as a single set of directions to follow to find a server, just as if you were trying to find a place in the real world. While there are dozens of record types, some are way more common than others

**A Record**:

* This is your pointer record, which works kind of like speed dial for a host. Records are the most common DNS records and are used to point a domain/subdomain to an IPv.4 address

**AAAA Record ("Quad-A Record")**:

* This is essentially an updated version of the A record built primarily for the IPv.6 address. So an A record is hardly wrong, but if both the A record and the AAAA record exist, the network will prefer the AAAA record

**CNAME Record ("Canonical Name Record")**:

* This record allows you to refer to a location by more than one name. A CNAME is used to map an alias to a domain name. Ex: mapping the subdomain 'www.' to the domain it's associated w/

**MX Record ("Mail Exchange/Mail Exchanger Record")**:

* The MX record is built to identify mail services; it specifies what server is responsible for handling email associated w/ the domain name. If there are multiple mail servers available, you can prioritize your records

**NS Record ("Nameserver Record")**:

* The NS record helps identify other nameservers in the DNS hierarchy

**TXT Record ("Text Record")**:

* A text record is used to store additional information

## DNS Propagation:

* Changing DNS records will take 24-48 hours to take effect across the Internet

* Every DNS record has a **TTL**, or **Time To Live**. TTL is what dictates how long a DNS record is cached in your local network or browser. WHM's default TTL setting is 14,400 seconds (4 hours). In the event that a user is migrating off of DoOO or switching to another DoOO server, editing the TTL ahead of time will reduce the time for propagation

## DNS FAQ

> *When should I point nameservers and when should I edit my existing A record?*

Both essentially accomplish the same task, but there are subtle differences that can make or break your domain-literally. Short answer: You'll want to edit nameservers when you want the third-party service provider to be in charge of the entire DNS zone for your domain. Meaning, subdomain 'test.labrumfield.com' and email address 'hello@labrumfield.com' would be pointed to the new hosting *in addition* to the TLD, 'labrumfield.com'. You would change the A record for a domain if you *only* want that specific domain (or subdomain/subfolder) to be pointed to the new location. So if I edited the A record for 'project.labrumfield.com' to point to Squarespace, I could still use 'labrumfield.com' at Reclaim Hosting

That said, you can still point all subdomains to a third-party service provider using an A record by creating a **Wildcard DNS record**. It would look like this: '*.labrumfield.com' pointed at '104.243.45.66', or whatever the new server IP is. The asterisk basically says point 'anything'.labrumfield.com to your server

> *Couldn't the user just transfer the domain to the third party service instead of changing nameservers?*

Yes, but ICANN requires that the domain is at least 60 days old b/c a successful transfer is initiated. So if a user wants to begin using their domain elsewhere before the 60-day mark, changing nameservers is a way to make that happen. It can also be helpful to change nameservers before a domain transfer is initiated to minimize downtime
