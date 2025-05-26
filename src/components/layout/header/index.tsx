"use client"

import { useState, useEffect } from "react"
import Link from "next/link"
import { usePathname } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Sheet, SheetContent, SheetTrigger } from "@/components/ui/sheet"
import {
  NavigationMenu,
  NavigationMenuLink,
  NavigationMenuItem,
  NavigationMenuList,
} from "@/components/ui/navigation-menu"
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select"
import { Menu, Zap, Globe } from "lucide-react"
import { cn } from "@/lib/utils"
import { useIntl } from "react-intl"
import { useLocale } from "@/i18n/provider"

const navigation = [
  { name: 'nav.home', href: "/" },
  { name: 'nav.about', href: "/about" },
  { name: 'nav.partners', href: "/partners" },
  { name: 'nav.contacts', href: "/contact" },
]

const languages = [
  { code: 'az', name: 'language.az' },
  { code: 'ru', name: 'language.ru' },
  { code: 'en', name: 'language.en' },
]

export default function Navbar() {
  const [isScrolled, setIsScrolled] = useState(false)
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false)
  const pathname = usePathname()
  const intl = useIntl()
  const { locale, setLocale } = useLocale()

  useEffect(() => {
    const handleScroll = () => {
      setIsScrolled(window.scrollY > 10)
    }
    window.addEventListener("scroll", handleScroll)
    return () => window.removeEventListener("scroll", handleScroll)
  }, [])

  return (
    <header
      className={cn(
        "fixed top-0 w-full z-50 transition-all duration-300",
        isScrolled ? "bg-white/95 backdrop-blur-md shadow-lg border-b border-slate-200/50" : "bg-transparent",
      )}
    >
      <div className="container mx-auto px-4">
        <div className="flex items-center justify-center h-16 lg:h-20 relative">
          <Link href="/" className="absolute left-4 flex items-center space-x-2 group">
            <div className="w-10 h-10 bg-gradient-to-br from-blue-600 to-purple-600 rounded-lg flex items-center justify-center group-hover:scale-105 transition-transform duration-200">
              <Zap className="h-6 w-6 text-white" />
            </div>
            <div className="flex flex-col">
              <span className="text-xl font-bold bg-gradient-to-r from-blue-600 to-purple-600 bg-clip-text text-transparent">
                ARM
              </span>
              <span className="text-xs text-muted-foreground -mt-1">SaaS Platform</span>
            </div>
          </Link>

          <nav className="hidden lg:flex items-center">
            <NavigationMenu>
              <NavigationMenuList>
                {navigation.map((item) => (
                  <NavigationMenuItem key={item.name}>
                    <Link href={item.href} legacyBehavior passHref>
                      <NavigationMenuLink
                        className={cn(
                          "group inline-flex h-10 w-max items-center justify-center rounded-md px-4 py-2 text-sm font-medium transition-colors hover:bg-slate-100 hover:text-slate-900 focus:bg-slate-100 focus:text-slate-900 focus:outline-none disabled:pointer-events-none disabled:opacity-50",
                          pathname === item.href ? "text-blue-600" : "text-slate-600",
                        )}
                      >
                        {intl.formatMessage({ id: item.name })}
                      </NavigationMenuLink>
                    </Link>
                  </NavigationMenuItem>
                ))}
              </NavigationMenuList>
            </NavigationMenu>
          </nav>

          {/* Language Selector */}
          <div className="absolute right-4 flex items-center space-x-2">
            <Select value={locale} onValueChange={(value: any) => setLocale(value)}>
              <SelectTrigger className="w-[140px]">
                <Globe className="mr-2 h-4 w-4" />
                <SelectValue>
                  {intl.formatMessage({ id: languages.find(lang => lang.code === locale)?.name })}
                </SelectValue>
              </SelectTrigger>
              <SelectContent>
                {languages.map((lang) => (
                  <SelectItem key={lang.code} value={lang.code}>
                    {intl.formatMessage({ id: lang.name })}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>

          {/* Mobile Menu Button */}
          <Sheet open={isMobileMenuOpen} onOpenChange={setIsMobileMenuOpen}>
            <SheetTrigger asChild>
              <Button variant="ghost" size="icon" className="lg:hidden absolute right-4">
                <Menu className="h-6 w-6" />
              </Button>
            </SheetTrigger>
            <SheetContent side="right" className="w-[300px] sm:w-[400px]">
              <div className="flex flex-col space-y-6 mt-6">
                <div className="flex items-center space-x-2">
                  <div className="w-8 h-8 bg-gradient-to-br from-blue-600 to-purple-600 rounded-lg flex items-center justify-center">
                    <Zap className="h-5 w-5 text-white" />
                  </div>
                  <span className="text-lg font-bold bg-gradient-to-r from-blue-600 to-purple-600 bg-clip-text text-transparent">
                    ARM SaaS
                  </span>
                </div>

                <nav className="flex flex-col space-y-4">
                  {navigation.map((item) => (
                    <Link
                      key={item.name}
                      href={item.href}
                      className={cn(
                        "text-lg font-medium transition-colors hover:text-blue-600",
                        pathname === item.href ? "text-blue-600" : "text-slate-600",
                      )}
                      onClick={() => setIsMobileMenuOpen(false)}
                    >
                      {intl.formatMessage({ id: item.name })}
                    </Link>
                  ))}
                </nav>

                {/* Mobile Language Selector */}
                <div className="pt-4 border-t">
                  <Select value={locale} onValueChange={(value: any) => setLocale(value)}>
                    <SelectTrigger className="w-full">
                      <Globe className="mr-2 h-4 w-4" />
                      <SelectValue>
                        {intl.formatMessage({ id: languages.find(lang => lang.code === locale)?.name })}
                      </SelectValue>
                    </SelectTrigger>
                    <SelectContent>
                      {languages.map((lang) => (
                        <SelectItem key={lang.code} value={lang.code}>
                          {intl.formatMessage({ id: lang.name })}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>
              </div>
            </SheetContent>
          </Sheet>
        </div>
      </div>
    </header>
  )
}
