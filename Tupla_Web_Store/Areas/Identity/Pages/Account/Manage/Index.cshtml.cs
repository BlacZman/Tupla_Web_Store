using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla.Data.Core.CustomerData;
using Tupla.Data.Core.PictureData;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHtmlHelper htmlHelper;
        private readonly ICustomerPicture picdb;
        private readonly IWebHostEnvironment env;
        private CustomerPicture userPic { get; set; }

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHtmlHelper htmlHelper,
            ICustomerPicture picdb,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.htmlHelper = htmlHelper;
            this.picdb = picdb;
            this.env = env;
        }

        public IEnumerable<SelectListItem> Genders { get; set; }
        public string Username { get; set; }
        public IFormFile imgfile { get; set; }
        public string imgDisplay { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            public string First_name { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "last name")]
            public string Last_name { get; set; }
            public Gender Gender { get; set; }
            [DataType(DataType.Date)]
            /*[Range(typeof(DateTime), "1/2/1920", "31/12/2017", 
                ErrorMessage = "Value for {0} must be between {1} and {2}")]*/
            [Display(Name = "Date of birth")]
            public DateTime Date_of_birth { get; set; }
            [DataType(DataType.PhoneNumber, ErrorMessage = "Please Enter a valid Phone Number")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [DataType(DataType.Text)]
            public string Address { get; set; }
            [DataType(DataType.PostalCode, ErrorMessage = "Please Enter a valid PIN/ZIP Code")]
            public string Zipcode { get; set; }
            [DataType(DataType.Text)]
            public string Country { get; set; }
            //public int CompanyID { get; set; }
            [DataType(DataType.DateTime)]
            [Display(Name = "Created since")]
            public DateTime UserCreateDate { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            userPic = picdb.GetIconById(Username);
            imgDisplay = userPic == null ? "~/img/notfound.jpg" : @"~/img/" + userPic.Path;
            Input = new InputModel
            {
                First_name = user.First_name,
                Last_name = user.Last_name,
                Gender = user.Gender,
                Date_of_birth = user.Date_of_birth,
                PhoneNumber = phoneNumber,
                Address = user.Address,
                Zipcode = user.Zipcode,
                Country = user.Country,
                UserCreateDate = user.UserCreateDate
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Genders = htmlHelper.GetEnumSelectList<Gender>();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                Genders = htmlHelper.GetEnumSelectList<Gender>();
                await LoadAsync(user);
                return Page();
            }

            //Image
            if (imgfile != null)
            {
                userPic = userPic == null ? new CustomerPicture() : userPic;
                //Upload to file system
                string uploadsFolder = Path.Combine(env.WebRootPath, "img");
                string uniqueFileName = Path.Combine("user", Guid.NewGuid().ToString() + "_" + imgfile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //Update database
                await Task.Run(() =>
                {
                    userPic.Path = uniqueFileName;
                    userPic.imageType = ImageType.Icon;
                    userPic.Username = user.UserName;
                });
                
                picdb.AddIcon(userPic);
                await picdb.CommitAsync();
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imgfile.CopyToAsync(fileStream);
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            if (Input.First_name != user.First_name) user.First_name = Input.First_name; ;
            if (Input.Last_name != user.Last_name) user.Last_name = Input.Last_name; ;
            if (Input.Gender != user.Gender) user.Gender = Input.Gender; ;
            if (Input.Date_of_birth != user.Date_of_birth) user.Date_of_birth = Input.Date_of_birth; ;
            if (Input.Address != user.Address) user.Address = Input.Address; ;
            if (Input.Zipcode != user.Zipcode) user.Zipcode = Input.Zipcode; ;
            if (Input.Country != user.Country) user.Country = Input.Country; ;

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
