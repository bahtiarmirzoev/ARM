import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { I18nProvider } from "@/i18n/provider";
import Navbar from "@/app/presentation/layout/header";
import Footer from "@/app/presentation/layout/footer";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  metadataBase: new URL("https://arm-service.com"),
  title: {
    default: "ARM - Auto Service Management SaaS Platform",
    template: "%s | ARM Auto Service",
  },
  description:
    "Modern SaaS platform for auto service management. Streamline your auto repair shop operations with our comprehensive management system.",
  keywords: [
    "auto service management",
    "car repair software",
    "auto shop management",
    "repair shop software",
    "automotive service platform",
    "auto repair management",
    "car service software",
    "automotive management system",
  ],
  authors: [{ name: "ARM Team" }],
  creator: "ARM Development Team",
  publisher: "ARM",
  formatDetection: {
    email: false,
    address: false,
    telephone: false,
  },
  openGraph: {
    type: "website",
    locale: "en_US",
    url: "https://arm-service.com",
    title: "ARM - Auto Service Management SaaS Platform",
    description:
      "Modern SaaS platform for auto service management. Streamline your auto repair shop operations with our comprehensive management system.",
    siteName: "ARM Auto Service",
    images: [
      {
        url: "/og-image.jpg",
        width: 1200,
        height: 630,
        alt: "ARM Auto Service Platform",
      },
    ],
  },
  twitter: {
    card: "summary_large_image",
    title: "ARM - Auto Service Management SaaS Platform",
    description:
      "Modern SaaS platform for auto service management. Streamline your auto repair shop operations with our comprehensive management system.",
    images: ["/twitter-image.jpg"],
    creator: "@arm_service",
  },
  robots: {
    index: true,
    follow: true,
    googleBot: {
      index: true,
      follow: true,
      "max-video-preview": -1,
      "max-image-preview": "large",
      "max-snippet": -1,
    },
  },
  verification: {
    google: "google-site-verification-code",
    yandex: "yandex-verification-code",
  },
  alternates: {
    canonical: null,
    languages: {
      "en-US": null,
      "ru-RU": null,
    },
  },
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en" suppressHydrationWarning>
      <head>
        <link rel="icon" href="/favicon.ico" />
        <link
          rel="apple-touch-icon"
          sizes="180x180"
          href="/apple-touch-icon.png"
        />
        <link
          rel="icon"
          type="image/png"
          sizes="32x32"
          href="/favicon-32x32.png"
        />
        <link
          rel="icon"
          type="image/png"
          sizes="16x16"
          href="/favicon-16x16.png"
        />
        <link rel="manifest" href="/site.webmanifest" />
        <meta name="theme-color" content="#ffffff" />
      </head>
      <body className={inter.className}>
        <I18nProvider>
          <div className="flex flex-col min-h-screen">
            <Navbar />
            <main className="flex-grow">{children}</main>
            <Footer />
          </div>
        </I18nProvider>
      </body>
    </html>
  );
}
