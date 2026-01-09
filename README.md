# 🤖 AI Tutor Website  
### ASP.NET Core MVC | Responsive Web Application | Role-Based Access Control

---

## 📌 Project Overview
The **AI Tutor Website** is a full-stack ASP.NET Core MVC web application developed as an academic project.  
The website introduces Artificial Intelligence concepts, enables users to upload and manage AI-generated images, and demonstrates modern web development practices including MVC architecture, authentication, authorization, and responsive design.

The project strictly follows academic and technical guidelines:
- Model–View–Controller (MVC) architecture
- External CSS and JavaScript only
- Responsive design for all devices
- Role-based access control
- Entity Framework Core integration

---

## 🌟 Core Features
- User Registration and Login
- Role-based access (Visitor, Member, Admin)
- AI Image upload and management (CRUD)
- Responsive user interface
- Shared navigation menu and footer
- Secure database interaction using Entity Framework Core
- Cross-browser compatibility (Chrome & Edge)

---

## 🛠️ Tech Stack

### 🔹 Backend
- **ASP.NET Core MVC (.NET 8)**
- **C#**
- **Entity Framework Core**
- **SQLite Database**

### 🔹 Frontend
- **HTML5**
- **CSS3**
- **Bootstrap 5**
- **JavaScript (ES6)**

### 🔹 Layout & Responsiveness
- **Bootstrap Grid System**
- **Flexbox**
- **CSS Grid**
- **CSS Media Queries**

### 🔹 Authentication & Security
- **ASP.NET Identity**
- **Role-based Authorization (Visitor, Member, Admin)**

### 🔹 UI & Assets
- **Font Awesome Icons**
- **External CSS and JavaScript files only**

### 🔹 Development Tools
- **Visual Studio 2022 / Visual Studio Code**
- **.NET 8 SDK**
- **Git & GitHub**

### 🔹 Browser Support
- **Google Chrome**
- **Microsoft Edge**

---

## 📁 Project Folder Structure
```
AITUTORWEBSITE/
│
├── Controllers/
│   ├── AccountController.cs
│   ├── AllImagesController.cs
│   ├── CreativeController.cs
│   └── HomeController.cs
│
├── Models/
│   ├── AllImage.cs
│   ├── User.cs
│   └── ErrorViewModel.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   │
│   ├── AllImages/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Details.cshtml
│   │   └── Delete.cshtml
│   │
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── Contact.cshtml
│   │   └── Privacy.cshtml
│   │
│   ├── Creative/
│   │   ├── Index.cshtml
│   │   └── Showcase.cshtml
│   │
│   └── Shared/
│       ├── _ViewImports.cshtml
│       └── _ViewStart.cshtml
│
├── wwwroot/
│   ├── css/
│   │   └── site.css
│   ├── js/
│   │   └── site.js
│   └── lib/
│
├── Program.cs
├── AITutorWebsite.csproj
├── mvc-project.sln
└── README.md
```

---

## 🔐 Authentication & Authorization
The website supports **three access levels**, enforced through authentication and authorization logic.

### 👤 Visitor (Not Logged In)
- Home page  
- Contact page  
- Creative page  
- Register & Login pages  

### 👥 Member (Logged-In User)
- All visitor pages  
- Create AI Images  
- View AI Image details  

### 🛡️ Admin
- Full access to the website  
- Create, Edit, and Delete AI Images  
- Manage all content  

---

## 🖼️ AI Images Module
The AI Images module allows users to manage AI-generated images using full CRUD functionality.

### Image Information Includes:
- Prompt  
- Image Generator  
- Upload Date  
- Filename  
- Like counter  

Entity Framework Core is used for database creation, migrations, and data access.

---

## 📄 Website Pages
- 🏠 **Home**
- 🖼️ **AI Images**
- 📞 **Contact**
- 🎨 **Creative Page**
- 🔐 **Register & Login**

---

## 📱 Responsive Design
The website is fully responsive and optimized for desktop, tablet, and mobile devices using Bootstrap, Flexbox, Grid, and media queries.

---

## ⚙️ How to Run the Project
```bash
dotnet restore
dotnet ef database update
dotnet run
```

---

## 📚 Academic Declaration
This project was developed for **academic purposes only**.  
All media used is either copyright-free or self-created.

---

## 👨‍💻 Author
Ahmed Ghaffar
www.linkedin.com/in/ahmed-ghaffar-504018271

---

## 📜 License
Educational Use Only
