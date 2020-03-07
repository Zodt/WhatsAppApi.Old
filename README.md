# WhatsAppApi



## What is WhatsAppApi?

WhatsAppApi is a cross-platform WhatsApp client library which may be used for the sending and reading of messages, working on chats, instances, queues and bans operation.
Library based on [chat-api](https://chat-api.com) service.

## Documentation

Before use WhatsAppApi learn chat-api's documentation. Ypu can found it at [here](https://app.chat-api.com/docs) {You need have chat-api account}.

## Using WhatsAppApi

### Sending Messages

One of the more common operations that WhatsAppApi is meant for is sending text messages.

```csharp
using System;
using TestWhatsAppApiClient.Properties;

using WhatsAppApi.Connect;
using WhatsAppApi.Operation;
using WhatsAppApi.Operation.Messages.OperationHelpers;

namespace TestWhatsAppApiClient
{
    internal class Program
    {
        public static WhatsAppConnect Connect { get; set; }

        private static void Main()
        {
            Connect = new WhatsAppConnect(Resources.WhatsApp_Server, Resources.WhatsApp_Instance, Resources.WhatsApp_Token); //to protect this data

            var messageOperation = new MessageOperation(Connect);
            var textMessage = new TextMessage
            {
                PhoneOrChatId = "+7(985) 462-44-18",
                Message = "Test TextMessage"
            };

            var response = messageOperation.SendMessage(textMessage).Result;
            Console.WriteLine(response);
        }
    }
}


```

#### Send file
Sending files is possible in 2 ways: using a link or using the local path to the file
##### Using Link
```csharp
            var sendFile = new SendFile
            {
                PhoneOrChatId = "+7(985) 462-44-18",
                ImagePathOrLink = "https://upload.wikimedia.org/wikipedia/ru/3/33/NatureCover2001.jpg",
                FileName = "TestFile"
            };

            var response = messageOperation.SendMessage(sendFile).Result;
```

##### Using local path to the file
```csharp
            var sendFile = new SendFile
            {
                PhoneOrChatId = "+7(985) 462-44-18",
                ImagePathOrLink = Resources.PathToTestPicture,,
                FileName = "TestFile"
            };

            var response = messageOperation.SendMessage(sendFile).Result;
```

#### Send Link

```csharp
            var sendLink = new SendLink
            {
                PhoneOrChatId = "+7(985) 462-44-18",
                Link = "https://upload.wikimedia.org/wikipedia/ru/3/33/NatureCover2001.jpg",
                Title = "Link's title"
            };

            var response = messageOperation.SendMessage(sendLink).Result;
```

Or if you want use another pictures
```csharp
            var sendLink = new SendLink
            {
                PhoneOrChatId = "+7(985) 462-44-18",
                Link = "https://upload.wikimedia.org/wikipedia/ru/3/33/NatureCover2001.jpg",
                PreviewBase64 = "https://www.throwbacks.com/content/images/2017/09/Untitled-design--63--1.png",
                Title = "Link's title"
            };

            var response = messageOperation.SendMessage(sendLink).Result;
```


#### Send contact

```csharp
            var sendContact = new SendContact
            {
                PhoneOrChatId = "+7(985) 462-44-18",
//ContactId by chat-api service is simple phone number and postfix "@c.us"
                ContactId = new List<string> { "74633123456@c.us" } 
            };

            var response = messageOperation.SendMessage(sendContact).Result;
```

#### Send location
```csharp
            var sendLocation = new SendLocation
            {
                PhoneOrChatId = "+7(985) 462-44-18",
                Address = @"Text under the message with the location.Supports two strings. To use two strings, use the '\n' character.",
                Longitude = 0,
                Latitude = 0
            };

            var response = messageOperation.SendMessage(sendLocation).Result;
```
