namespace ARM.Common.Templates;

public static class ConfirmEmailTemplate
{
    public const string CommonStyles = @"
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            margin: 0;
            padding: 0;
            background-color: #f8f9fa;
        }
        .container {
            max-width: 600px;
            margin: 20px auto;
            padding: 0;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }
        .header {
            text-align: center;
            padding: 30px 20px;
            background: linear-gradient(135deg, #1a73e8 0%, #0d47a1 100%);
            margin: 0;
        }
        .header h1 {
            color: #ffffff;
            margin: 0;
            font-size: 28px;
            font-weight: 600;
        }
        .content {
            padding: 40px 30px;
            color: #202124;
        }
        .button-container {
            text-align: center;
            margin: 30px 0;
        }
        .button {
            display: inline-block;
            padding: 14px 32px;
            background: linear-gradient(135deg, #1a73e8 0%, #0d47a1 100%);
            color: #ffffff;
            text-decoration: none;
            border-radius: 8px;
            font-weight: 600;
            font-size: 16px;
            transition: transform 0.2s, box-shadow 0.2s;
            box-shadow: 0 2px 4px rgba(26, 115, 232, 0.3);
        }
        .button:hover {
            transform: translateY(-1px);
            box-shadow: 0 4px 8px rgba(26, 115, 232, 0.4);
        }
        .link-text {
            background-color: #f8f9fa;
            padding: 15px;
            border-radius: 8px;
            border: 1px solid #e8eaed;
            margin: 20px 0;
            word-break: break-all;
            color: #5f6368;
            font-size: 14px;
        }
        .warning {
            background-color: #fef7e0;
            border-left: 4px solid #fbbc04;
            padding: 15px;
            margin: 20px 0;
            border-radius: 0 8px 8px 0;
        }
        .footer {
            text-align: center;
            padding: 30px;
            color: #5f6368;
            font-size: 14px;
            background-color: #f8f9fa;
            border-top: 1px solid #e8eaed;
        }";

    public static string GetEnglishTemplate(string verifyUrl)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Verify Your Email</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Verify Your Email</h1>
        </div>
        <div class='content'>
            <p style='font-size: 16px; margin-bottom: 20px;'>To ensure the security of your account, please verify your email address by clicking the button below:</p>
            
            <div class='button-container'>
                <a href='{verifyUrl}' class='button'>Verify Email Address</a>
            </div>
            
            <p style='color: #5f6368; font-size: 14px;'>If the button above doesn't work, copy and paste this link into your browser:</p>
            <div class='link-text'>{verifyUrl}</div>
            
            <div class='warning'>
                <p style='margin: 0; color: #202124;'><strong>Important:</strong> This verification link will expire in 5 minutes for security reasons.</p>
            </div>
            
            <p style='color: #5f6368; font-size: 14px;'>If you didn't request this verification, please ignore this email or contact support if you have concerns.</p>
        </div>
        <div class='footer'>
            <p style='margin: 0;'>This is an automated message, please do not reply to this email.</p>
            <p style='margin: 10px 0 0 0; color: #1a73e8;'>© {DateTime.Now.Year} ARM. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
    }

    public static string GetRussianTemplate(string verifyUrl)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Подтверждение Email</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Подтверждение Email</h1>
        </div>
        <div class='content'>
            <p style='font-size: 16px; margin-bottom: 20px;'>Для обеспечения безопасности вашей учетной записи, пожалуйста, подтвердите ваш email, нажав на кнопку ниже:</p>
            
            <div class='button-container'>
                <a href='{verifyUrl}' class='button'>Подтвердить Email</a>
            </div>
            
            <p style='color: #5f6368; font-size: 14px;'>Если кнопка выше не работает, скопируйте и вставьте эту ссылку в ваш браузер:</p>
            <div class='link-text'>{verifyUrl}</div>
            
            <div class='warning'>
                <p style='margin: 0; color: #202124;'><strong>Важно:</strong> Ссылка для подтверждения действительна в течение 5 минут по соображениям безопасности.</p>
            </div>
            
            <p style='color: #5f6368; font-size: 14px;'>Если вы не запрашивали это подтверждение, проигнорируйте это письмо или обратитесь в службу поддержки, если у вас есть опасения.</p>
        </div>
        <div class='footer'>
            <p style='margin: 0;'>Это автоматическое сообщение, пожалуйста, не отвечайте на него.</p>
            <p style='margin: 10px 0 0 0; color: #1a73e8;'>© {DateTime.Now.Year} ARM. Все права защищены.</p>
        </div>
    </div>
</body>
</html>";
    }

    public static string GetAzerbaijaniTemplate(string verifyUrl)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>E-poçt Təsdiqi</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>E-poçt Təsdiqi</h1>
        </div>
        <div class='content'>
            <p style='font-size: 16px; margin-bottom: 20px;'>Hesabınızın təhlükəsizliyini təmin etmək üçün, aşağıdakı düyməni klikləyərək e-poçt ünvanınızı təsdiqləyin:</p>
            
            <div class='button-container'>
                <a href='{verifyUrl}' class='button'>Təsdiqlə</a>
            </div>
            
            <p style='color: #5f6368; font-size: 14px;'>Yuxarıdakı düymə işləmirsə, bu linki brauzerinizə kopyalayıb yapışdırın:</p>
            <div class='link-text'>{verifyUrl}</div>
            
            <div class='warning'>
                <p style='margin: 0; color: #202124;'><strong>Vacib:</strong> Təsdiq linki təhlükəsizlik səbəblərinə görə 5 dəqiqə ərzində etibarlıdır.</p>
            </div>
            
            <p style='color: #5f6368; font-size: 14px;'>Bu təsdiqi siz tələb etməmisinizsə, bu e-poçtu nəzərə almayın və ya narahatlıqlarınız varsa dəstək xidmətinə müraciət edin.</p>
        </div>
        <div class='footer'>
            <p style='margin: 0;'>Bu avtomatik mesajdır, zəhmət olmasa cavab verməyin.</p>
            <p style='margin: 10px 0 0 0; color: #1a73e8;'>© {DateTime.Now.Year} ARM. Bütün hüquqlar qorunur.</p>
        </div>
    </div>
</body>
</html>";
    }

    public static string GetTemplate(string verifyUrl, string lang = "en")
    {
        return lang switch
        {
            "az" => GetAzerbaijaniTemplate(verifyUrl),
            "ru" => GetRussianTemplate(verifyUrl),
            _ => GetEnglishTemplate(verifyUrl),
        };
    }
};