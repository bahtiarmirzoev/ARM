'use client';

import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { usePathname } from "next/navigation";
import Link from "next/link";
import { 
  HomeIcon, 
  LayoutDashboardIcon, 
  Settings, 
  LogOut,
  CarFront,
  FileBarChart
} from "lucide-react";

const routes = [
  {
    label: 'Dashboard',
    icon: LayoutDashboardIcon,
    href: '/dashboard',
    color: "text-sky-500",
    description: "Overview and analytics"
  },
 
  {
    label: 'Autoservices',
    icon: CarFront,
    href: '/autoservices',
    color: "text-violet-500",
    description: "Manage auto services"
  },
  {
    label: 'Requests',
    icon: FileBarChart,
    href: '/requests',
    color: "text-amber-500",
    description: "View partnership requests"
  },
];

export function Sidebar() {
  const pathname = usePathname();

  return (
    <div className="space-y-4 py-4 flex flex-col h-full bg-[#111827] text-white">
      <div className="px-3 py-2 flex-1">
        <Link href="/" className="flex items-center pl-3 mb-14">
          <div className="relative w-8 h-8 mr-4 rounded-lg bg-white/10 flex items-center justify-center">
            <CarFront className="w-5 h-5 text-white" />
          </div>
          <h1 className="text-2xl font-bold">
            AutoAdmin
          </h1>
        </Link>
        <div className="space-y-2">
          {routes.map((route) => (
            <Link
              key={route.href}
              href={route.href}
              className={cn(
                "text-sm group flex p-3 w-full justify-start font-medium cursor-pointer hover:text-white hover:bg-white/10 rounded-lg transition-all duration-300 ease-in-out",
                pathname === route.href ? "text-white bg-white/10" : "text-zinc-400",
              )}
            >
              <div className="flex items-center flex-1">
                <div className={cn(
                  "flex items-center justify-center w-10 h-10 rounded-lg transition-all duration-300 group-hover:bg-white/10",
                  pathname === route.href ? "bg-white/10" : "transparent"
                )}>
                  <route.icon className={cn("h-5 w-5", route.color)} />
                </div>
                <div className="flex flex-col ml-4">
                  <span className="font-medium">{route.label}</span>
                  <span className="text-xs text-zinc-500">{route.description}</span>
                </div>
              </div>
              {pathname === route.href && (
                <div className="absolute left-0 w-1 h-8 my-auto rounded-r-full bg-white" />
              )}
            </Link>
          ))}
        </div>
      </div>
      <div className="px-3 py-2 mt-auto border-t border-gray-800">
        <Link
          href="/settings"
          className={cn(
            "text-sm group flex p-3 w-full justify-start font-medium cursor-pointer hover:text-white hover:bg-white/10 rounded-lg transition-all duration-300 ease-in-out",
            pathname === "/settings" ? "text-white bg-white/10" : "text-zinc-400",
          )}
        >
          <div className="flex items-center flex-1">
            <div className={cn(
              "flex items-center justify-center w-10 h-10 rounded-lg transition-all duration-300 group-hover:bg-white/10",
              pathname === "/settings" ? "bg-white/10" : "transparent"
            )}>
              <Settings className="h-5 w-5 text-gray-500" />
            </div>
            <div className="flex flex-col ml-4">
              <span className="font-medium">Settings</span>
              <span className="text-xs text-zinc-500">System preferences</span>
            </div>
          </div>
          {pathname === "/settings" && (
            <div className="absolute left-0 w-1 h-8 my-auto rounded-r-full bg-white" />
          )}
        </Link>
        <Button 
          variant="ghost" 
          className="w-full mt-2 justify-start text-sm p-3 text-zinc-400 hover:text-white hover:bg-white/10 transition-all duration-300"
          onClick={() => {
            // Add your logout logic here
            console.log("Logout clicked");
          }}
        >
          <div className="flex items-center justify-center w-10 h-10 rounded-lg">
            <LogOut className="h-5 w-5" />
          </div>
          <div className="flex flex-col ml-4">
            <span className="font-medium">Logout</span>
            <span className="text-xs text-zinc-500">Exit system</span>
          </div>
        </Button>
      </div>
    </div>
  );
} 