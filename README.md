# email-loop
Looping app to send same email over and over.

Recently a certain individual constantly sent me non-relevant emails several times each day.  I asked him to please stop and remove my email address from his list and he refused.  So this little project was the answer.  It is just a simple little app that will send an email then delay and send it again... on and on until the intervals were complete.  I set the code up and one last time asked the email intruder to stop but he refused.  So I fired up my little utility and set it up to send him an email every 5 seconds.  After about 100 emails he kindly agreed to remove my email address from his list.  


Make sure you edit the servers.json file.  It is a dictionary saved  Dictionary<string, SmtpServer>

Also note that if you are going to use the SMTP from Google you need to visit https://myaccount.google.com/lesssecureapps to make your Google account less secure or the connection will be rejected.  


Other references are
MimeKit
MailKit.Net.Smtp
MailKit.Security
