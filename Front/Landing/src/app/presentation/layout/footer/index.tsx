"use client";

import Link from "next/link";
import { Button } from "@/app/presentation/components/ui/button";
import { Input } from "@/app/presentation/components/ui/input";
import { Badge } from "@/app/presentation/components/ui/badge";
import {
  Zap,
  Mail,
  Phone,
  MapPin,
  Facebook,
  Twitter,
  Linkedin,
  Instagram,
  Youtube,
  Send,
  Car,
  Users,
  BarChart3,
  Shield,
  Clock,
  Headphones,
  Github,
} from "lucide-react";
import { useIntl } from "react-intl";

export default function Footer() {
  const intl = useIntl();

  const footerSections = [
    {
      title: intl.formatMessage({ id: "footer.company" }),
      links: [
        { name: intl.formatMessage({ id: "footer.about" }), href: "/about" },
        {
          name: intl.formatMessage({ id: "footer.careers" }),
          href: "/careers",
        },
        { name: intl.formatMessage({ id: "footer.blog" }), href: "/blog" },
        { name: intl.formatMessage({ id: "footer.press" }), href: "/press" },
      ],
    },
    {
      title: intl.formatMessage({ id: "footer.product" }),
      links: [
        {
          name: intl.formatMessage({ id: "footer.features" }),
          href: "/#features",
        },
        {
          name: intl.formatMessage({ id: "footer.pricing" }),
          href: "/pricing",
        },
        { name: intl.formatMessage({ id: "footer.demo" }), href: "/demo" },
        { name: intl.formatMessage({ id: "footer.api" }), href: "/api" },
      ],
    },
    {
      title: intl.formatMessage({ id: "footer.resources" }),
      links: [
        {
          name: intl.formatMessage({ id: "footer.documentation" }),
          href: "/docs",
        },
        { name: intl.formatMessage({ id: "footer.guides" }), href: "/guides" },
        {
          name: intl.formatMessage({ id: "footer.support" }),
          href: "/support",
        },
        { name: intl.formatMessage({ id: "footer.status" }), href: "/status" },
      ],
    },
    {
      title: intl.formatMessage({ id: "footer.legal" }),
      links: [
        {
          name: intl.formatMessage({ id: "footer.privacy" }),
          href: "/privacy",
        },
        { name: intl.formatMessage({ id: "footer.terms" }), href: "/terms" },
        {
          name: intl.formatMessage({ id: "footer.license" }),
          href: "/license",
        },
      ],
    },
  ];

  const socialLinks = [
    {
      icon: <Facebook className="h-5 w-5" />,
      href: "https://facebook.com/arm-saas-az",
    },
    {
      icon: <Twitter className="h-5 w-5" />,
      href: "https://twitter.com/arm_saas_az",
    },
    {
      icon: <Instagram className="h-5 w-5" />,
      href: "https://instagram.com/arm.saas.az",
    },
    {
      icon: <Linkedin className="h-5 w-5" />,
      href: "https://linkedin.com/company/arm-saas-az",
    },
    {
      icon: <Github className="h-5 w-5" />,
      href: "https://github.com/arm-saas-az",
    },
  ];

  return (
    <footer className="bg-slate-900 text-white">
      {/* Newsletter Section */}
      <div className="border-b border-slate-800">
        <div className="container mx-auto px-4 py-12">
          <div className="max-w-4xl mx-auto text-center">
            <h2 className="text-2xl lg:text-3xl font-bold mb-4">
              {intl.formatMessage({ id: "footer.newsletter.title" })}
            </h2>
            <p className="text-slate-300 mb-8 leading-relaxed">
              {intl.formatMessage({ id: "footer.newsletter.description" })}
            </p>
            <div className="flex flex-col sm:flex-row gap-4 max-w-md mx-auto">
              <Input
                type="email"
                placeholder={intl.formatMessage({
                  id: "footer.newsletter.placeholder",
                })}
                className="bg-slate-800 border-slate-700 text-white placeholder:text-slate-400 focus:border-blue-500"
              />
              <Button className="bg-blue-600 hover:bg-blue-700 shrink-0">
                <Send className="h-4 w-4 mr-2" />
                {intl.formatMessage({ id: "footer.newsletter.button" })}
              </Button>
            </div>
          </div>
        </div>
      </div>

      {/* Main Footer Content */}
      <div className="container mx-auto px-4 py-16">
        <div className="grid lg:grid-cols-5 gap-8">
          {/* Company Info */}
          <div className="lg:col-span-2">
            <div className="flex items-center space-x-2 mb-6">
              <div className="w-12 h-12 bg-gradient-to-br from-blue-600 to-purple-600 rounded-lg flex items-center justify-center">
                <Zap className="h-7 w-7 text-white" />
              </div>
              <div>
                <span className="text-2xl font-bold bg-gradient-to-r from-blue-400 to-purple-400 bg-clip-text text-transparent">
                  ARM
                </span>
                <div className="text-sm text-slate-400">SaaS Platform</div>
              </div>
            </div>
            <p className="text-slate-300 mb-6 leading-relaxed">
              {intl.formatMessage({ id: "footer.company.description" })}
            </p>
            <div className="space-y-3">
              <div className="flex items-center space-x-3 text-slate-300">
                <MapPin className="h-5 w-5 text-blue-400" />
                <span>{intl.formatMessage({ id: "contacts.address" })}</span>
              </div>
              <div className="flex items-center space-x-3 text-slate-300">
                <Phone className="h-5 w-5 text-blue-400" />
                <span>{intl.formatMessage({ id: "contacts.phone" })}</span>
              </div>
              <div className="flex items-center space-x-3 text-slate-300">
                <Mail className="h-5 w-5 text-blue-400" />
                <span>{intl.formatMessage({ id: "contacts.email" })}</span>
              </div>
              <div className="flex items-center space-x-3 text-slate-300">
                <Clock className="h-5 w-5 text-blue-400" />
                <span>{intl.formatMessage({ id: "contacts.workHours" })}</span>
              </div>
            </div>
          </div>

          {/* Company Links */}
          <div>
            <h3 className="text-lg font-semibold mb-6">
              {intl.formatMessage({ id: "footer.company" })}
            </h3>
            <ul className="space-y-3">
              {footerSections[0].links.map((link) => (
                <li key={link.name}>
                  <Link
                    href={link.href}
                    className="text-slate-300 hover:text-white transition-colors duration-200 hover:translate-x-1 transform inline-block"
                  >
                    {link.name}
                  </Link>
                </li>
              ))}
            </ul>
          </div>

          {/* Product Links */}
          <div>
            <h3 className="text-lg font-semibold mb-6">
              {intl.formatMessage({ id: "footer.product" })}
            </h3>
            <ul className="space-y-3">
              {footerSections[1].links.map((link) => (
                <li key={link.name}>
                  <Link
                    href={link.href}
                    className="text-slate-300 hover:text-white transition-colors duration-200 flex items-center space-x-2 group"
                  >
                    <span className="group-hover:translate-x-1 transform transition-transform duration-200">
                      {link.name}
                    </span>
                  </Link>
                </li>
              ))}
            </ul>
          </div>

          {/* Support Links */}
          <div>
            <h3 className="text-lg font-semibold mb-6">
              {intl.formatMessage({ id: "footer.resources" })}
            </h3>
            <ul className="space-y-3">
              {footerSections[2].links.map((link) => (
                <li key={link.name}>
                  <Link
                    href={link.href}
                    className="text-slate-300 hover:text-white transition-colors duration-200 flex items-center space-x-2 group"
                  >
                    <span className="group-hover:translate-x-1 transform transition-transform duration-200">
                      {link.name}
                    </span>
                  </Link>
                </li>
              ))}
            </ul>
            <div className="mt-6">
              <Badge
                className="border-green-500 text-green-400"
              >
                <div className="w-2 h-2 bg-green-400 rounded-full mr-2 animate-pulse"></div>
                {intl.formatMessage({ id: "footer.system.status" })}
              </Badge>
            </div>
          </div>
        </div>
      </div>

      {/* Bottom Footer */}
      <div className="border-t border-slate-800">
        <div className="container mx-auto px-4 py-8">
          <div className="flex flex-col lg:flex-row items-center justify-between space-y-4 lg:space-y-0">
            <div className="flex flex-col sm:flex-row items-center space-y-2 sm:space-y-0 sm:space-x-6 text-slate-400 text-sm">
              <span>{intl.formatMessage({ id: "footer.copyright" })}</span>
              <div className="flex space-x-4">
                {footerSections[3].links.map((link) => (
                  <Link
                    key={link.name}
                    href={link.href}
                    className="hover:text-white transition-colors duration-200"
                  >
                    {link.name}
                  </Link>
                ))}
              </div>
            </div>
            <div className="flex items-center space-x-4">
              <span className="text-slate-400 text-sm">
                {intl.formatMessage({ id: "footer.socials" })}:
              </span>
              <div className="flex space-x-3">
                {socialLinks.map((social) => (
                  <a
                    key={social.icon.props.className}
                    href={social.href}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="w-10 h-10 bg-slate-800 rounded-lg flex items-center justify-center text-slate-400 hover:text-white hover:bg-slate-700 transition-all duration-200 hover:scale-110"
                    aria-label={social.icon.props.className}
                  >
                    {social.icon}
                  </a>
                ))}
              </div>
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
}
