"use client";

import { createContext, useContext, useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { authAPI } from "@/lib/api";
import { User } from "@/types";

interface AuthContextType {
  user: User | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  isLoading: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    checkAuth();
  }, []);

  const checkAuth = async () => {
    try {
      const token = localStorage.getItem("token");
      if (token) {
        // Временная заглушка - создаем мок пользователя
        const mockUser: User = {
          id: "1",
          name: "Администратор",
          email: "admin@example.com",
          role: "admin",
          isActive: true,
          emailVerified: true,
          createdAt: new Date().toISOString(),
          permissions: ["user_manage", "role_manage", "system_settings"],
        };
        setUser(mockUser);
      }
    } catch (error) {
      console.error("Auth check failed:", error);
      localStorage.removeItem("token");
    } finally {
      setIsLoading(false);
    }
  };

  const login = async (email: string, password: string) => {
    try {
      // Временная заглушка - принимаем любые данные
      console.log("Login attempt:", { email, password });
      
      // Создаем мок токен и пользователя
      const mockToken = "mock-jwt-token-" + Date.now();
      const mockUser: User = {
        id: "1",
        name: email.includes("admin") ? "Администратор" : email.includes("manager") ? "Менеджер" : "Пользователь",
        email: email,
        role: email.includes("admin") ? "admin" : email.includes("manager") ? "manager" : "customer",
        isActive: true,
        emailVerified: true,
        createdAt: new Date().toISOString(),
        permissions: email.includes("admin") 
          ? ["user_manage", "role_manage", "system_settings"] 
          : email.includes("manager") 
          ? ["customer_manage", "order_manage", "service_manage"]
          : ["order_view", "profile_manage"],
      };

      localStorage.setItem("token", mockToken);
      setUser(mockUser);
      router.push("/dashboard");
    } catch (error) {
      console.error("Login failed:", error);
      throw error;
    }
  };

  const logout = () => {
    localStorage.removeItem("token");
    setUser(null);
    router.push("/login");
  };

  return (
    <AuthContext.Provider value={{ user, login, logout, isLoading }}>
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}; 