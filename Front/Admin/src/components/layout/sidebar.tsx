"use client";

import { useState } from "react";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { useAuth } from "@/providers/auth-provider";
import {
  HomeIcon,
  UsersIcon,
  WrenchScrewdriverIcon,
  Cog6ToothIcon,
  ChartBarIcon,
  CalendarIcon,
  DocumentTextIcon,
  UserGroupIcon,
  ShieldCheckIcon,
  BuildingOfficeIcon,
  ClockIcon,
} from "@heroicons/react/24/outline";
import { cn } from "@/lib/utils";

const navigation = {
  customer: [
    { name: "Главная", href: "/dashboard", icon: HomeIcon },
    { name: "Мои автомобили", href: "/dashboard/cars", icon: WrenchScrewdriverIcon },
    { name: "Заказы на ремонт", href: "/dashboard/repair-orders", icon: WrenchScrewdriverIcon },
    { name: "История ремонтов", href: "/dashboard/repair-history", icon: DocumentTextIcon },
    { name: "Отзывы", href: "/dashboard/reviews", icon: ChartBarIcon },
    { name: "Профиль", href: "/dashboard/profile", icon: Cog6ToothIcon },
  ],
  manager: [
    { name: "Главная", href: "/dashboard", icon: HomeIcon },
    { name: "Клиенты", href: "/dashboard/customers", icon: UsersIcon },
    { name: "Автомобили", href: "/dashboard/cars", icon: WrenchScrewdriverIcon },
    { name: "Заказы на ремонт", href: "/dashboard/repair-orders", icon: WrenchScrewdriverIcon },
    { name: "Услуги", href: "/dashboard/services", icon: Cog6ToothIcon },
    { name: "Рабочие часы", href: "/dashboard/working-hours", icon: ClockIcon },
    { name: "Отзывы", href: "/dashboard/reviews", icon: ChartBarIcon },
    { name: "Отчеты", href: "/dashboard/reports", icon: DocumentTextIcon },
  ],
  admin: [
    { name: "Главная", href: "/dashboard", icon: HomeIcon },
    { name: "Пользователи", href: "/dashboard/users", icon: UsersIcon },
    { name: "Роли и разрешения", href: "/dashboard/roles", icon: ShieldCheckIcon },
    { name: "Автосервисы", href: "/dashboard/brands", icon: BuildingOfficeIcon },
    { name: "Клиенты", href: "/dashboard/customers", icon: UserGroupIcon },
    { name: "Автомобили", href: "/dashboard/cars", icon: WrenchScrewdriverIcon },
    { name: "Заказы на ремонт", href: "/dashboard/repair-orders", icon: WrenchScrewdriverIcon },
    { name: "Услуги", href: "/dashboard/services", icon: Cog6ToothIcon },
    { name: "Рабочие часы", href: "/dashboard/working-hours", icon: ClockIcon },
    { name: "Отзывы", href: "/dashboard/reviews", icon: ChartBarIcon },
    { name: "Отчеты", href: "/dashboard/reports", icon: DocumentTextIcon },
    { name: "Настройки", href: "/dashboard/settings", icon: Cog6ToothIcon },
  ],
};

export function Sidebar() {
  const [collapsed, setCollapsed] = useState(false);
  const pathname = usePathname();
  const { user, logout } = useAuth();

  const getNavigationItems = () => {
    if (!user) return [];
    
    switch (user.role.toLowerCase()) {
      case "customer":
        return navigation.customer;
      case "manager":
        return navigation.manager;
      case "admin":
        return navigation.admin;
      default:
        return navigation.customer;
    }
  };

  const navItems = getNavigationItems();

  return (
    <div className={cn(
      "flex flex-col bg-white border-r border-gray-200 transition-all duration-300",
      collapsed ? "w-16" : "w-64"
    )}>
      {/* Logo */}
      <div className="flex items-center justify-between h-16 px-4 border-b border-gray-200">
        {!collapsed && (
          <div className="flex items-center space-x-2">
            <WrenchScrewdriverIcon className="h-8 w-8 text-blue-600" />
            <span className="text-xl font-bold text-gray-900">ARM</span>
          </div>
        )}
        <button
          onClick={() => setCollapsed(!collapsed)}
          className="p-1 rounded-md hover:bg-gray-100"
        >
          <svg className="h-5 w-5 text-gray-500" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
          </svg>
        </button>
      </div>

      {/* Navigation */}
      <nav className="flex-1 px-2 py-4 space-y-1">
        {navItems.map((item) => {
          const isActive = pathname === item.href;
          return (
            <Link
              key={item.name}
              href={item.href}
              className={cn(
                "flex items-center px-2 py-2 text-sm font-medium rounded-md transition-colors",
                isActive
                  ? "bg-blue-100 text-blue-700"
                  : "text-gray-600 hover:bg-gray-50 hover:text-gray-900"
              )}
            >
              <item.icon className="h-5 w-5 mr-3" />
              {!collapsed && item.name}
            </Link>
          );
        })}
      </nav>

      {/* User Profile */}
      {!collapsed && user && (
        <div className="border-t border-gray-200 p-4">
          <div className="flex items-center">
            <div className="h-8 w-8 rounded-full bg-blue-100 flex items-center justify-center">
              <span className="text-sm font-medium text-blue-600">
                {user.name.charAt(0).toUpperCase()}
              </span>
            </div>
            <div className="ml-3">
              <p className="text-sm font-medium text-gray-700">{user.name}</p>
              <p className="text-xs text-gray-500">{user.role}</p>
            </div>
          </div>
          <button
            onClick={logout}
            className="mt-3 w-full text-left text-sm text-gray-600 hover:text-gray-900"
          >
            Выйти
          </button>
        </div>
      )}
    </div>
  );
} 