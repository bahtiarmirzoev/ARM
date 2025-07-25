"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { useAuth } from "@/providers/auth-provider";
import { Button } from "@/components/ui/button";
import { User } from "@/types";
import {
  PlusIcon,
  MagnifyingGlassIcon,
  FunnelIcon,
  EllipsisVerticalIcon,
} from "@heroicons/react/24/outline";
import toast from "react-hot-toast";

export default function UsersPage() {
  const { user: currentUser } = useAuth();
  const [page, setPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedRole, setSelectedRole] = useState("all");
  const [selectedStatus, setSelectedStatus] = useState("all");

  // Мок данные пользователей
  const mockUsers: User[] = [
    {
      id: "1",
      name: "Администратор Системы",
      email: "admin@arm-service.com",
      role: "admin",
      isActive: true,
      emailVerified: true,
      lastLogin: "2024-01-15 14:30",
      createdAt: "2024-01-01",
      permissions: ["user_manage", "role_manage", "system_settings"],
    },
    {
      id: "2",
      name: "Менеджер Сервиса",
      email: "manager@arm-service.com",
      role: "manager",
      isActive: true,
      emailVerified: true,
      lastLogin: "2024-01-15 12:15",
      createdAt: "2024-01-05",
      permissions: ["customer_manage", "order_manage", "service_manage"],
    },
    {
      id: "3",
      name: "Иван Петров",
      email: "ivan.petrov@example.com",
      role: "customer",
      isActive: true,
      emailVerified: true,
      lastLogin: "2024-01-15 10:45",
      createdAt: "2024-01-10",
      permissions: ["order_view", "profile_manage"],
    },
    {
      id: "4",
      name: "Мария Сидорова",
      email: "maria.sidorova@example.com",
      role: "customer",
      isActive: false,
      emailVerified: false,
      createdAt: "2024-01-12",
      permissions: ["order_view", "profile_manage"],
    },
  ];

  const users = mockUsers;
  const totalPages = 1;

  const handleDeleteUser = async (userId: string) => {
    if (window.confirm("Вы уверены, что хотите удалить этого пользователя?")) {
      toast.success("Пользователь успешно удален");
    }
  };

  const handleToggleStatus = async (userId: string, currentStatus: boolean) => {
    toast.success(`Пользователь ${currentStatus ? "деактивирован" : "активирован"}`);
  };

  const getRoleText = (role: string) => {
    switch (role) {
      case "admin":
        return "Администратор";
      case "manager":
        return "Менеджер";
      case "customer":
        return "Клиент";
      default:
        return role;
    }
  };

  const getRoleColor = (role: string) => {
    switch (role) {
      case "admin":
        return "bg-red-100 text-red-800";
      case "manager":
        return "bg-blue-100 text-blue-800";
      case "customer":
        return "bg-green-100 text-green-800";
      default:
        return "bg-gray-100 text-gray-800";
    }
  };

  const getStatusColor = (isActive: boolean) => {
    return isActive
      ? "bg-green-100 text-green-800"
      : "bg-red-100 text-red-800";
  };

  const getStatusText = (isActive: boolean) => {
    return isActive ? "Активен" : "Неактивен";
  };

  const filteredUsers = users.filter((user: User) => {
    const matchesSearch = user.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         user.email.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesRole = selectedRole === "all" || user.role === selectedRole;
    const matchesStatus = selectedStatus === "all" || 
                         (selectedStatus === "active" && user.isActive) ||
                         (selectedStatus === "inactive" && !user.isActive);
    
    return matchesSearch && matchesRole && matchesStatus;
  });

  return (
    <DashboardLayout>
      <div className="space-y-6">
        {/* Header */}
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Пользователи</h1>
            <p className="text-gray-600">
              Управление пользователями системы
            </p>
          </div>
          <Button className="flex items-center gap-2">
            <PlusIcon className="h-4 w-4" />
            Добавить пользователя
          </Button>
        </div>

        {/* Filters */}
        <div className="bg-white rounded-lg shadow p-6">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
            {/* Search */}
            <div className="relative">
              <MagnifyingGlassIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
              <input
                type="text"
                placeholder="Поиск по имени или email..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="pl-10 pr-4 py-2 w-full border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              />
            </div>

            {/* Role Filter */}
            <select
              value={selectedRole}
              onChange={(e) => setSelectedRole(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            >
              <option value="all">Все роли</option>
              <option value="admin">Администратор</option>
              <option value="manager">Менеджер</option>
              <option value="customer">Клиент</option>
            </select>

            {/* Status Filter */}
            <select
              value={selectedStatus}
              onChange={(e) => setSelectedStatus(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            >
              <option value="all">Все статусы</option>
              <option value="active">Активные</option>
              <option value="inactive">Неактивные</option>
            </select>

            {/* Clear Filters */}
            <Button
              onClick={() => {
                setSearchTerm("");
                setSelectedRole("all");
                setSelectedStatus("all");
              }}
              className="flex items-center gap-2"
            >
              <FunnelIcon className="h-4 w-4" />
              Сбросить
            </Button>
          </div>
        </div>

        {/* Users Table */}
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Пользователь
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Email
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Роль
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Статус
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Последний вход
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Действия
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {filteredUsers.map((user: User) => (
                  <tr key={user.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <div className="flex-shrink-0 h-10 w-10">
                          <div className="h-10 w-10 rounded-full bg-gray-300 flex items-center justify-center">
                            <span className="text-sm font-medium text-gray-700">
                              {user.name.charAt(0).toUpperCase()}
                            </span>
                          </div>
                        </div>
                        <div className="ml-4">
                          <div className="text-sm font-medium text-gray-900">
                            {user.name}
                          </div>
                        </div>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {user.email}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getRoleColor(user.role)}`}>
                        {getRoleText(user.role)}
                      </span>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(user.isActive)}`}>
                        {getStatusText(user.isActive)}
                      </span>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {user.lastLogin ? new Date(user.lastLogin).toLocaleDateString() : "Никогда"}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                      <div className="flex items-center space-x-2">
                        <Button
                          onClick={() => handleToggleStatus(user.id, user.isActive)}
                          disabled={currentUser?.id === user.id}
                        >
                          {user.isActive ? "Деактивировать" : "Активировать"}
                        </Button>
                        {currentUser?.role === "admin" && currentUser?.id !== user.id && (
                          <Button
                            onClick={() => handleDeleteUser(user.id)}
                          >
                            Удалить
                          </Button>
                        )}
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
} 