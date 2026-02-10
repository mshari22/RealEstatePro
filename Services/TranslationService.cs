namespace RealEstatePro.Services;

public interface ITranslationService
{
    Dictionary<string, string> GetTranslations(string language);
    IEnumerable<string> GetSupportedLanguages();
}

public class TranslationService : ITranslationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _translations;
    
    public TranslationService()
    {
        _translations = new Dictionary<string, Dictionary<string, string>>
        {
            ["ar"] = new Dictionary<string, string>
            {
                // Navigation
                ["nav_home"] = "الرئيسية",
                ["nav_browse"] = "تصفح العقارات",
                ["nav_login"] = "تسجيل الدخول",
                ["nav_signup"] = "إنشاء حساب",
                ["nav_dashboard"] = "إضافة عقار",
                ["nav_logout"] = "خروج",
                ["my_properties"] = "عقاراتي",
                
                // Home Page
                ["hero_title"] = "ابحث عن عقارك المثالي في الرياض",
                ["hero_subtitle"] = "اكتشف أفضل الفلل والشقق والأراضي للبيع والإيجار في الرياض والمملكة",
                ["search_placeholder"] = "ابحث بالحي أو نوع العقار...",
                ["btn_search"] = "بحث",
                ["featured_properties"] = "عقارات مميزة في الرياض",
                ["view_all"] = "عرض الكل",
                ["view_details"] = "عرض التفاصيل",
                
                // Property Types
                ["for_sale"] = "للبيع",
                ["for_rent"] = "للإيجار",
                ["sale"] = "للبيع",
                ["rent"] = "للإيجار",
                ["apartment"] = "شقة",
                ["villa"] = "فيلا",
                ["land"] = "أرض",
                ["commercial"] = "تجاري",
                ["floor"] = "دور",
                ["duplex"] = "دوبلكس",
                
                // Property Details
                ["bedrooms"] = "غرف نوم",
                ["bathrooms"] = "دورات مياه",
                ["area"] = "المساحة",
                ["sqm"] = "م²",
                ["price"] = "السعر",
                ["location"] = "الموقع",
                ["description"] = "الوصف",
                ["contact_owner"] = "تواصل مع المعلن",
                ["sar"] = "ر.س",
                ["property_type_label"] = "نوع العرض",
                ["back_to_list"] = "العودة للقائمة",
                ["per_year"] = "/سنوياً",
                
                // Forms
                ["email"] = "البريد الإلكتروني",
                ["password"] = "كلمة المرور",
                ["confirm_password"] = "تأكيد كلمة المرور",
                ["full_name"] = "الاسم الكامل",
                ["phone"] = "رقم الجوال",
                ["message"] = "نص الرسالة",
                ["submit"] = "إرسال",
                ["login"] = "دخول",
                ["signup"] = "تسجيل",
                ["or"] = "أو",
                ["already_have_account"] = "لديك حساب؟",
                ["dont_have_account"] = "ليس لديك حساب؟",
                
                // Dashboard / Add Property
                ["add_property"] = "إضافة عقار جديد",
                ["property_title"] = "عنوان الإعلان",
                ["property_type"] = "نوع العرض",
                ["category"] = "تصنيف العقار",
                ["select_location"] = "حدد الموقع على الخريطة",
                ["click_map_hint"] = "اضغط على الخريطة لتحديد الموقع",
                ["upload_image"] = "رابط صورة العقار",
                
                // Messages
                ["success"] = "تمت العملية بنجاح",
                ["error"] = "حدث خطأ",
                ["loading"] = "جاري التحميل...",
                ["no_properties"] = "لا توجد عقارات حالياً",
                ["be_first"] = "كن أول من يضيف عقاراً!",
                
                // Filter
                ["min_price"] = "أقل سعر",
                ["max_price"] = "أعلى سعر",
                ["all_categories"] = "جميع الأنواع",
                
                // Footer
                ["footer_rights"] = "جميع الحقوق محفوظة",
                ["footer_about"] = "عن المنصة",
                ["footer_about_desc"] = "منصة عقارية سعودية لعرض وبحث العقارات",
                ["footer_contact"] = "تواصل معنا",
                ["footer_links"] = "روابط سريعة",
                ["footer_privacy"] = "سياسة الخصوصية"
            },
            
            ["en"] = new Dictionary<string, string>
            {
                // Navigation
                ["nav_home"] = "Home",
                ["nav_browse"] = "Browse",
                ["nav_login"] = "Login",
                ["nav_signup"] = "Sign Up",
                ["nav_dashboard"] = "Add Property",
                ["nav_logout"] = "Logout",
                ["my_properties"] = "My Properties",
                
                // Home Page
                ["hero_title"] = "Find Your Dream Property in Riyadh",
                ["hero_subtitle"] = "Discover villas, apartments, and land for sale and rent across Saudi Arabia",
                ["search_placeholder"] = "Search by district or property type...",
                ["btn_search"] = "Search",
                ["featured_properties"] = "Featured Properties in Riyadh",
                ["view_all"] = "View All",
                ["view_details"] = "View Details",
                
                // Property Types
                ["for_sale"] = "For Sale",
                ["for_rent"] = "For Rent",
                ["sale"] = "For Sale",
                ["rent"] = "For Rent",
                ["apartment"] = "Apartment",
                ["villa"] = "Villa",
                ["land"] = "Land",
                ["commercial"] = "Commercial",
                ["floor"] = "Floor",
                ["duplex"] = "Duplex",
                
                // Property Details
                ["bedrooms"] = "Bedrooms",
                ["bathrooms"] = "Bathrooms",
                ["area"] = "Area",
                ["sqm"] = "m²",
                ["price"] = "Price",
                ["location"] = "Location",
                ["description"] = "Description",
                ["contact_owner"] = "Contact Owner",
                ["sar"] = "SAR",
                ["property_type_label"] = "Type",
                ["back_to_list"] = "← Back to List",
                ["per_year"] = "/year",
                
                // Forms
                ["email"] = "Email",
                ["password"] = "Password",
                ["confirm_password"] = "Confirm Password",
                ["full_name"] = "Full Name",
                ["phone"] = "Phone Number",
                ["message"] = "Message",
                ["submit"] = "Submit",
                ["login"] = "Login",
                ["signup"] = "Sign Up",
                ["or"] = "or",
                ["already_have_account"] = "Already have an account?",
                ["dont_have_account"] = "Don't have an account?",
                
                // Dashboard / Add Property
                ["add_property"] = "Add New Property",
                ["property_title"] = "Property Title",
                ["property_type"] = "Listing Type",
                ["category"] = "Property Category",
                ["select_location"] = "Select Location on Map",
                ["click_map_hint"] = "Click on the map to set location",
                ["upload_image"] = "Property Image URL",
                
                // Messages
                ["success"] = "Success",
                ["error"] = "Error",
                ["loading"] = "Loading...",
                ["no_properties"] = "No properties found",
                ["be_first"] = "Be the first to add a property!",
                
                // Filter
                ["min_price"] = "Min Price",
                ["max_price"] = "Max Price",
                ["all_categories"] = "All Categories",
                
                // Footer
                ["footer_rights"] = "All rights reserved",
                ["footer_about"] = "About",
                ["footer_about_desc"] = "Saudi real estate platform for property listing and search",
                ["footer_contact"] = "Contact Us",
                ["footer_links"] = "Quick Links",
                ["footer_privacy"] = "Privacy Policy"
            }
        };
    }
    
    public Dictionary<string, string> GetTranslations(string language)
    {
        if (_translations.TryGetValue(language.ToLower(), out var translations))
        {
            return translations;
        }
        
        // Default to English
        return _translations["en"];
    }
    
    public IEnumerable<string> GetSupportedLanguages()
    {
        return _translations.Keys;
    }
}
