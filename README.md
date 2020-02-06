# email-loop
Looping app to send same email over and over


Make sure you edit the SmtpServer.json file.  It is a dictionary saved  Dictionary<string, SmtpServer>

Also note that if you are going to use the SMTP from Google you need to visit https://myaccount.google.com/lesssecureapps to make your Google account less secure or the connection will be rejected.  


Other references are
MimeKit
MailKit.Net.Smtp
MailKit.Security
