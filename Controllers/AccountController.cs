using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AITutorWebsite.Controllers
{
    public class AccountController : Controller
    {
        // Store registered users in session for demo purposes
        // Pre-register the admin user
        private static readonly Dictionary<string, (string Name, string Email, string Password, string Role)> _registeredUsers = new()
        {
            { "admin@asp.net", ("Admin User", "admin@asp.net", "Admin@123", "Admin") }
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Please enter both email and password.";
                return View();
            }

            // Check if user is registered
            if (!_registeredUsers.ContainsKey(email.ToLower()))
            {
                TempData["Error"] = "User not found. Please register first before logging in.";
                return View();
            }

            var user = _registeredUsers[email.ToLower()];
            if (user.Password != password)
            {
                TempData["Error"] = "Invalid password.";
                return View();
            }

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["Message"] = $"Welcome back, {user.Name}!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string name, string email, string password, string confirmPassword, string role = "Member")
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Please fill in all required fields.";
                return View();
            }

            // Prevent registration with admin email
            if (email.ToLower() == "admin@asp.net")
            {
                TempData["Error"] = "This email address is reserved and cannot be used for registration.";
                return View();
            }

            if (password != confirmPassword)
            {
                TempData["Error"] = "Passwords do not match.";
                return View();
            }

            if (password.Length < 6)
            {
                TempData["Error"] = "Password must be at least 6 characters long.";
                return View();
            }

            // Check if user already exists
            if (_registeredUsers.ContainsKey(email.ToLower()))
            {
                TempData["Error"] = "User with this email already exists.";
                return View();
            }

            // Register user (always as Member role for security)
            _registeredUsers[email.ToLower()] = (name, email, password, "Member");

            // Auto-login after registration
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Role, "Member")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["Message"] = $"Registration successful! Welcome, {name}!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "You have been logged out successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}