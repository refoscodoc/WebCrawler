# WebCrawler
A simple WebCrawler built specifically for fly540.com

The web crowler was created with an API endpoint that can accept a date in dd-MM-yyyy format and return the deisred reults. In case the date is left empty, it will return the current date. I decided to create it this way as Swagger is a great tool for running an app, and it gives a bit more flexibility than packing everything in Program.cs and run at startup with the default, current day, date.

The only package used is HtmlAgilityPack, used for the Html parsing.

Through a console line the cheapest fare will be indicated. Please note that it picks the cheapest at the earliest time and date and keeps that as the preferenced one.

The .pcap Wireshark network data is included in the traffic_capture.pcap file.
