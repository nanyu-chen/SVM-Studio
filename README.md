# SVM Studio - Elegant Photography & Styling Website

A sophisticated, full-stack ASP.NET Core MVC website for SVM Studio, a high-end photography and styling studio. Features an elegant, dreamy aesthetic with modern responsive design, smooth animations, and comprehensive content management capabilities.

![SVM Studio](https://via.placeholder.com/800x400/B8A9C9/FFFFFF?text=SVM+Studio)

## ✨ Features

### 🎨 **Elegant Design**
- Dreamy, artistic aesthetic with pastel color palette
- Responsive design that works on all devices
- Smooth animations and transitions using AOS (Animate On Scroll)
- Custom typography with Playfair Display and Lato fonts
- Professional photography gallery with lightbox functionality

### 🏠 **Core Pages**
- **Hero Home Page** - Stunning landing with featured work
- **About/Team** - Studio story and team member profiles
- **Gallery** - Filterable portfolio with categories
- **Services** - Detailed service offerings with packages
- **Blog** - Inspiration articles and behind-the-scenes content
- **Booking/Contact** - Professional inquiry forms

### ⚙️ **Admin Dashboard**
- Content management for portfolio items
- Blog post creation and editing
- Service management
- Booking and inquiry management
- User authentication with ASP.NET Identity

### 🛠 **Technical Stack**
- **Backend**: ASP.NET Core 9.0 MVC
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, Custom CSS, JavaScript
- **Animations**: AOS (Animate On Scroll), Custom transitions
- **Icons**: Font Awesome

## 🚀 Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- Any modern code editor (VS Code recommended)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/nanyu-chen/SVM-Studio.git
   cd svm-studio
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Create the database**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open your browser**
   Navigate to `https://localhost:5148` or `http://localhost:5148`

## 📁 Project Structure

```
SVMStudio/
├── Controllers/          # MVC Controllers
│   ├── HomeController.cs
│   └── AdminController.cs
├── Data/                # Database Context
│   └── ApplicationDbContext.cs
├── Models/              # Data Models
│   └── StudioModels.cs
├── ViewModels/          # View Models
│   └── ViewModels.cs
├── Views/               # Razor Views
│   ├── Home/
│   ├── Admin/
│   └── Shared/
├── wwwroot/             # Static Files
│   ├── css/
│   ├── js/
│   └── lib/
├── Migrations/          # EF Migrations
└── appsettings.json     # Configuration
```

## 🎯 Key Features Explained

### **Elegant Design System**
The website uses a carefully crafted design system with:
- CSS custom properties for consistent theming
- Lavender and icy blue color palette
- Professional typography hierarchy
- Subtle animations and hover effects

### **Gallery System**
- Filterable portfolio by category (Wedding, Portrait, Commercial, etc.)
- Lightbox modal for image viewing
- Responsive grid layout
- Hover effects with overlay information

### **Booking System**
- Comprehensive booking form with validation
- Event-specific fields for weddings
- Email notifications
- Admin dashboard for managing inquiries

### **Content Management**
- Admin panel for managing all content
- Portfolio item CRUD operations
- Blog post management
- Service offerings management

## 🔧 Development

### **Adding New Features**
1. Create models in `Models/StudioModels.cs`
2. Update the database context in `Data/ApplicationDbContext.cs`
3. Add migrations: `dotnet ef migrations add YourMigrationName`
4. Update database: `dotnet ef database update`

### **Customizing Styles**
- Main styles are in `wwwroot/css/site.css`
- Uses CSS custom properties for easy theming
- Bootstrap 5 for responsive grid system
- Custom animations and transitions

### **Adding Admin Features**
- Controllers inherit from base controller
- Views use `_AdminLayout.cshtml`
- Admin-specific styles in `wwwroot/css/admin.css`

## 📱 Responsive Design

The website is fully responsive and optimized for:
- **Desktop** (1200px+)
- **Tablet** (768px - 1199px)
- **Mobile** (< 768px)

## 🌟 Performance Features

- Optimized images and assets
- Minimal JavaScript bundle
- Efficient CSS with custom properties
- Lazy loading for gallery images
- SEO-friendly markup

## 🛡️ Security

- ASP.NET Core Identity for authentication
- CSRF protection on forms
- Input validation and sanitization
- Secure database connections

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📞 Support

For support, email support@svmstudio.com or create an issue in this repository.

## 🙏 Acknowledgments

- Typography by Google Fonts (Playfair Display, Lato)
- Icons by Font Awesome
- Animations by AOS Library
- Bootstrap for responsive framework

---

**Built with ❤️ for photographers and creative professionals**
