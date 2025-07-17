"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { Button } from "@/components/ui/button";
import {
  PlusIcon,
  MagnifyingGlassIcon,
  FunnelIcon,
  EyeIcon,
  PencilIcon,
  TrashIcon,
  ShieldCheckIcon,
  UserGroupIcon,
} from "@heroicons/react/24/outline";
import toast from "react-hot-toast";

interface Role {
  id: string;
  name: string;
  description: string;
  permissions: string[];
  userCount: number;
  isActive: boolean;
  createdAt: string;
}

interface Permission {
  id: string;
  name: string;
  description: string;
  category: string;
}

export default function RolesPage() {
  const [page, setPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedStatus, setSelectedStatus] = useState("all");
  const [showPermissions, setShowPermissions] = useState<string | null>(null);

  // Мок данные ролей
  const mockRoles: Role[] = [
    {
      id: "1",
      name: "Администратор",
      description: "Полный доступ ко всем функциям системы",
      permissions: ["user_manage", "role_manage", "system_settings", "customer_manage", "order_manage", "service_manage", "report_view"],
      userCount: 2,
      isActive: true,
      createdAt: "2024-01-01",
    },
    {
      id: "2",
      name: "Менеджер",
      description: "Управление клиентами, заказами и услугами",
      permissions: ["customer_manage", "order_manage", "service_manage", "report_view"],
      userCount: 5,
      isActive: true,
      createdAt: "2024-01-02",
    },
    {
      id: "3",
      name: "Клиент",
      description: "Базовый доступ для просмотра своих заказов",
      permissions: ["order_view", "profile_manage"],
      userCount: 150,
      isActive: true,
      createdAt: "2024-01-03",
    },
    {
      id: "4",
      name: "Техник",
      description: "Доступ к техническим функциям",
      permissions: ["order_manage", "service_manage"],
      userCount: 8,
      isActive: false,
      createdAt: "2024-01-04",
    },
  ];

  // Мок данные разрешений
  const mockPermissions: Permission[] = [
    { id: "user_manage", name: "Управление пользователями", description: "Создание, редактирование и удаление пользователей", category: "Пользователи" },
    { id: "role_manage", name: "Управление ролями", description: "Создание и редактирование ролей и разрешений", category: "Безопасность" },
    { id: "system_settings", name: "Системные настройки", description: "Доступ к системным настройкам", category: "Система" },
    { id: "customer_manage", name: "Управление клиентами", description: "Создание, редактирование и удаление клиентов", category: "Клиенты" },
    { id: "order_manage", name: "Управление заказами", description: "Создание, редактирование и удаление заказов", category: "Заказы" },
    { id: "service_manage", name: "Управление услугами", description: "Создание, редактирование и удаление услуг", category: "Услуги" },
    { id: "report_view", name: "Просмотр отчетов", description: "Доступ к отчетам и аналитике", category: "Отчеты" },
    { id: "order_view", name: "Просмотр заказов", description: "Просмотр своих заказов", category: "Заказы" },
    { id: "profile_manage", name: "Управление профилем", description: "Редактирование собственного профиля", category: "Профиль" },
  ];

  const roles = mockRoles;
  const permissions = mockPermissions;
  const totalPages = 1;

  const handleDeleteRole = async (roleId: string) => {
    if (window.confirm("Вы уверены, что хотите удалить эту роль?")) {
      toast.success("Роль успешно удалена");
    }
  };

  const handleToggleStatus = async (roleId: string, currentStatus: boolean) => {
    toast.success(`Роль ${currentStatus ? "деактивирована" : "активирована"}`);
  };

  const getStatusColor = (isActive: boolean) => {
    return isActive
      ? "bg-green-100 text-green-800"
      : "bg-red-100 text-red-800";
  };

  const getStatusText = (isActive: boolean) => {
    return isActive ? "Активна" : "Неактивна";
  };

  const getPermissionName = (permissionId: string) => {
    const permission = permissions.find(p => p.id === permissionId);
    return permission ? permission.name : permissionId;
  };

  const getPermissionCategory = (permissionId: string) => {
    const permission = permissions.find(p => p.id === permissionId);
    return permission ? permission.category : "Другое";
  };

  const filteredRoles = roles.filter((role: Role) => {
    const matchesSearch = role.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         role.description.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesStatus = selectedStatus === "all" || 
                         (selectedStatus === "active" && role.isActive) ||
                         (selectedStatus === "inactive" && !role.isActive);
    
    return matchesSearch && matchesStatus;
  });

  const categories = Array.from(new Set(permissions.map(p => p.category)));

  return (
    <DashboardLayout>
      <div className="space-y-6">
        {/* Header */}
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Роли и разрешения</h1>
            <p className="text-gray-600">
              Управление ролями пользователей и их разрешениями
            </p>
          </div>
          <Button className="flex items-center gap-2">
            <PlusIcon className="h-4 w-4" />
            Добавить роль
          </Button>
        </div>

        {/* Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <ShieldCheckIcon className="h-8 w-8 text-blue-600" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Всего ролей
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {roles.length}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <UserGroupIcon className="h-8 w-8 text-green-600" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Активных ролей
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {roles.filter(r => r.isActive).length}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <ShieldCheckIcon className="h-8 w-8 text-purple-600" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Всего разрешений
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {permissions.length}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <UserGroupIcon className="h-8 w-8 text-orange-600" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Всего пользователей
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {roles.reduce((sum, r) => sum + r.userCount, 0)}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        {/* Filters */}
        <div className="bg-white rounded-lg shadow p-6">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            {/* Search */}
            <div className="relative">
              <MagnifyingGlassIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
              <input
                type="text"
                placeholder="Поиск по названию или описанию..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="pl-10 pr-4 py-2 w-full border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              />
            </div>

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
              variant="outline"
              onClick={() => {
                setSearchTerm("");
                setSelectedStatus("all");
              }}
              className="flex items-center gap-2"
            >
              <FunnelIcon className="h-4 w-4" />
              Сбросить
            </Button>
          </div>
        </div>

        {/* Roles Table */}
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Роль
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Пользователи
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Разрешения
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Статус
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Действия
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {filteredRoles.map((role: Role) => (
                  <>
                    <tr key={role.id} className="hover:bg-gray-50">
                      <td className="px-6 py-4 whitespace-nowrap">
                        <div className="flex items-center">
                          <div className="flex-shrink-0 h-10 w-10">
                            <div className="h-10 w-10 rounded-full bg-gray-300 flex items-center justify-center">
                              <ShieldCheckIcon className="h-5 w-5 text-gray-600" />
                            </div>
                          </div>
                          <div className="ml-4">
                            <div className="text-sm font-medium text-gray-900">
                              {role.name}
                            </div>
                            <div className="text-sm text-gray-500">
                              {role.description}
                            </div>
                          </div>
                        </div>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                        {role.userCount} пользователей
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap">
                        <div className="flex items-center">
                          <span className="text-sm text-gray-900">
                            {role.permissions.length} разрешений
                          </span>
                          <button
                            onClick={() => setShowPermissions(showPermissions === role.id ? null : role.id)}
                            className="ml-2 text-blue-600 hover:text-blue-900 text-sm"
                          >
                            {showPermissions === role.id ? "Скрыть" : "Показать"}
                          </button>
                        </div>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap">
                        <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(role.isActive)}`}>
                          {getStatusText(role.isActive)}
                        </span>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                        <div className="flex space-x-2">
                          <button className="text-blue-600 hover:text-blue-900">
                            <EyeIcon className="h-4 w-4" />
                          </button>
                          <button className="text-indigo-600 hover:text-indigo-900">
                            <PencilIcon className="h-4 w-4" />
                          </button>
                          <button 
                            onClick={() => handleToggleStatus(role.id, role.isActive)}
                            className="text-yellow-600 hover:text-yellow-900"
                          >
                            {role.isActive ? "Деактивировать" : "Активировать"}
                          </button>
                          <button 
                            onClick={() => handleDeleteRole(role.id)}
                            className="text-red-600 hover:text-red-900"
                          >
                            <TrashIcon className="h-4 w-4" />
                          </button>
                        </div>
                      </td>
                    </tr>
                    {showPermissions === role.id && (
                      <tr>
                        <td colSpan={5} className="px-6 py-4 bg-gray-50">
                          <div className="space-y-3">
                            <h4 className="text-sm font-medium text-gray-900">Разрешения роли "{role.name}":</h4>
                            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-3">
                              {role.permissions.map((permissionId) => {
                                const permission = permissions.find(p => p.id === permissionId);
                                if (!permission) return null;
                                
                                return (
                                  <div key={permissionId} className="bg-white p-3 rounded-lg border">
                                    <div className="text-sm font-medium text-gray-900">
                                      {permission.name}
                                    </div>
                                    <div className="text-xs text-gray-500 mt-1">
                                      {permission.description}
                                    </div>
                                    <div className="text-xs text-blue-600 mt-1">
                                      {permission.category}
                                    </div>
                                  </div>
                                );
                              })}
                            </div>
                          </div>
                        </td>
                      </tr>
                    )}
                  </>
                ))}
              </tbody>
            </table>
          </div>

          {/* Pagination */}
          {totalPages > 1 && (
            <div className="bg-white px-4 py-3 flex items-center justify-between border-t border-gray-200 sm:px-6">
              <div className="flex-1 flex justify-between sm:hidden">
                <button
                  onClick={() => setPage(Math.max(1, page - 1))}
                  disabled={page === 1}
                  className="relative inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 disabled:opacity-50"
                >
                  Назад
                </button>
                <button
                  onClick={() => setPage(Math.min(totalPages, page + 1))}
                  disabled={page === totalPages}
                  className="ml-3 relative inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 disabled:opacity-50"
                >
                  Вперед
                </button>
              </div>
              <div className="hidden sm:flex-1 sm:flex sm:items-center sm:justify-between">
                <div>
                  <p className="text-sm text-gray-700">
                    Показано <span className="font-medium">{((page - 1) * 10) + 1}</span> до{" "}
                    <span className="font-medium">{Math.min(page * 10, filteredRoles.length)}</span> из{" "}
                    <span className="font-medium">{filteredRoles.length}</span> результатов
                  </p>
                </div>
                <div>
                  <nav className="relative z-0 inline-flex rounded-md shadow-sm -space-x-px">
                    <button
                      onClick={() => setPage(Math.max(1, page - 1))}
                      disabled={page === 1}
                      className="relative inline-flex items-center px-2 py-2 rounded-l-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 disabled:opacity-50"
                    >
                      Назад
                    </button>
                    <button
                      onClick={() => setPage(Math.min(totalPages, page + 1))}
                      disabled={page === totalPages}
                      className="relative inline-flex items-center px-2 py-2 rounded-r-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 disabled:opacity-50"
                    >
                      Вперед
                    </button>
                  </nav>
                </div>
              </div>
            </div>
          )}
        </div>
      </div>
    </DashboardLayout>
  );
} 