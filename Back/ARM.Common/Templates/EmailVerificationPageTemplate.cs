namespace ARM.Common.Templates;

public static class EmailVerificationPageTemplate
{
    public const string CommonStyles = @"
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            margin: 0;
            padding: 0;
            background-color: #f8f9fa;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }
        .container {
            max-width: 600px;
            margin: 20px;
            padding: 40px;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            text-align: center;
        }
        .icon {
            width: 64px;
            height: 64px;
            margin-bottom: 20px;
        }
        .success-icon {
            color: #34a853;
        }
        .error-icon {
            color: #ea4335;
        }
        h2 {
            color: #202124;
            margin: 0 0 20px 0;
            font-size: 24px;
            font-weight: 600;
        }
        p {
            color: #5f6368;
            margin: 0 0 20px 0;
            font-size: 16px;
        }
        .button {
            display: inline-block;
            padding: 12px 24px;
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
        }";

    public static string GetSuccessTemplate(string lang = "en")
    {
        return lang switch
        {
            "ru" => GetRussianSuccessTemplate(),
            "az" => GetAzerbaijaniSuccessTemplate(),
            _ => GetEnglishSuccessTemplate()
        };
    }

    public static string GetErrorTemplate(string message, string lang = "en")
    {
        return lang switch
        {
            "ru" => GetRussianErrorTemplate(message),
            "az" => GetAzerbaijaniErrorTemplate(message),
            _ => GetEnglishErrorTemplate(message)
        };
    }

    private static string GetEnglishSuccessTemplate()
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Email Verified</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <svg class='icon success-icon' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z' fill='currentColor'/>
        </svg>
        <h2>Email Successfully Verified</h2>
        <p>Your email address has been successfully verified. You can now close this window and continue using the application.</p>
        <a href='/' class='button'>Return to Home</a>
    </div>
</body>
</html>";
    }

    private static string GetRussianSuccessTemplate()
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Email Подтвержден</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <svg class='icon success-icon' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z' fill='currentColor'/>
        </svg>
        <h2>Email Успешно Подтвержден</h2>
        <p>Ваш email адрес был успешно подтвержден. Теперь вы можете закрыть это окно и продолжить использование приложения.</p>
        <a href='/' class='button'>Вернуться на Главную</a>
    </div>
</body>
</html>";
    }

    private static string GetAzerbaijaniSuccessTemplate()
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>E-poçt Təsdiqləndi</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <svg class='icon success-icon' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z' fill='currentColor'/>
        </svg>
        <h2>E-poçt Uğurla Təsdiqləndi</h2>
        <p>E-poçt ünvanınız uğurla təsdiqləndi. İndi bu pəncərəni bağlaya və tətbiqetmədən istifadə etməyə davam edə bilərsiniz.</p>
        <a href='/' class='button'>Ana Səhifəyə Qayıt</a>
    </div>
</body>
</html>";
    }

    private static string GetEnglishErrorTemplate(string message)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Verification Error</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <svg class='icon error-icon' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z' fill='currentColor'/>
        </svg>
        <h2>Verification Failed</h2>
        <p>An error occurred during confirmation. Try again later</p>
        <a href='/' class='button'>Return to Home</a>
    </div>
</body>
</html>";
    }

    private static string GetRussianErrorTemplate(string message)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Ошибка Подтверждения</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <svg class='icon error-icon' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z' fill='currentColor'/>
        </svg>
        <h2>Ошибка Подтверждения</h2>
        <p>Произошла ошибка при подтверждении. Попробуйте позже</p>
        <a href='/' class='button'>Вернуться на Главную</a>
    </div>
</body>
</html>";
    }

    private static string GetAzerbaijaniErrorTemplate(string message)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Təsdiq Xətası</title>
    <style>{CommonStyles}</style>
</head>
<body>
    <div class='container'>
        <svg class='icon error-icon' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z' fill='currentColor'/>
        </svg>
        <h2>Təsdiq Uğursuz Oldu</h2>
        <p>Təsdiqdə səhv oldu. Daha sonra cəhd edin</p>
        <a href='/' class='button'>Ana Səhifəyə Qayıt</a>
    </div>
</body>
</html>";
    }
}