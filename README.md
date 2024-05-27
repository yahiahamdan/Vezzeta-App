# Algoriza-internship-290

## Description

🎉 Vezeeta: Streamlining healthcare with easy appointment booking, information access, and efficient CRUD operations for seamless user experience. 

## Database Design

<div style="text-align:center;">
  <img src="https://cdn.discordapp.com/attachments/443452026739752960/1183413783276834849/image.png?ex=65883ef8&is=6575c9f8&hm=112fd8b8964eb6815bf9dac89be286153a24f352c2bead1ffa77b7c49103c7d8&" alt="Database design" />
</div>

## Installation

```bash
$ git clone https://gitlab.com/MahmoudSerag/algoriza-internship-290.git
cd Vezzeta-App
```

## Database backup

- [Database Backup](https://drive.google.com/file/d/1CCLtE1VI2wAWm_pxauyUqPXzsw8CRxqK/view?usp=drive_link)

## Restore Dependencies

```bash
$ dotnet restore
```

## Entity Framework Core Migrations

```bash
$ dotnet ef database update
```

## Environment variables

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-Database-Connection"
  },
  "Jwt": {
    "SecretKey": "Your-Secret-Key",
    "Issuer": "Your-Issuer",
    "Audience": "Your-Audience"
  },
  "MailSettings": {
    "RealMail": "sragmahmoud4@gmail.com",
    "host": "smtp.gmail.com",
    "Password": "Your Password",
    "Port": 587
  }
}
```

## License

[MIT licensed](LICENSE).
