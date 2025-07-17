"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { Button } from "@/components/ui/button";
import { WorkingHour } from "@/types";
import {
  PlusIcon,
  PencilIcon,
  TrashIcon,
  ClockIcon,
} from "@heroicons/react/24/outline";
import toast from "react-hot-toast";

export default function WorkingHoursPage() {
  const [editingId, setEditingId] = useState<string | null>(null);

  // Мок данные рабочих часов
  const mockWorkingHours: WorkingHour[] = [
    {
      id: "1",
      brandId: "1",
      dayOfWeek: 1, // Понедельник
      openTime: "09:00",
      closeTime: "18:00",
      isOpen: true,
    },
    {
      id: "2",
      brandId: "1",
      dayOfWeek: 2, // Вторник
      openTime: "09:00",
      closeTime: "18:00",
      isOpen: true,
    },
    {
      id: "3",
      brandId: "1",
      dayOfWeek: 3, // Среда
      openTime: "09:00",
      closeTime: "18:00",
      isOpen: true,
    },
    {
      id: "4",
      brandId: "1",
      dayOfWeek: 4, // Четверг
      openTime: "09:00",
      closeTime: "18:00",
      isOpen: true,
    },
    {
      id: "5",
      brandId: "1",
      dayOfWeek: 5, // Пятница
      openTime: "09:00",
      closeTime: "18:00",
      isOpen: true,
    },
    {
      id: "6",
      brandId: "1",
      dayOfWeek: 6, // Суббота
      openTime: "10:00",
      closeTime: "16:00",
      isOpen: true,
    },
    {
      id: "7",
      brandId: "1",
      dayOfWeek: 0, // Воскресенье
      openTime: "00:00",
      closeTime: "00:00",
      isOpen: false,
    },
  ];

  const [workingHours, setWorkingHours] = useState<WorkingHour[]>(mockWorkingHours);

  const dayNames = [
    "Воскресенье",
    "Понедельник", 
    "Вторник",
    "Среда",
    "Четверг",
    "Пятница",
    "Суббота"
  ];

  const handleSave = (id: string, updatedData: Partial<WorkingHour>) => {
    setWorkingHours(prev => 
      prev.map(wh => 
        wh.id === id ? { ...wh, ...updatedData } : wh
      )
    );
    setEditingId(null);
    toast.success("Рабочие часы обновлены");
  };

  const handleDelete = (id: string) => {
    if (window.confirm("Вы уверены, что хотите удалить эти рабочие часы?")) {
      setWorkingHours(prev => prev.filter(wh => wh.id !== id));
      toast.success("Рабочие часы удалены");
    }
  };

  const getStatusColor = (isOpen: boolean) => {
    return isOpen
      ? "bg-green-100 text-green-800"
      : "bg-red-100 text-red-800";
  };

  const getStatusText = (isOpen: boolean) => {
    return isOpen ? "Открыто" : "Закрыто";
  };

  return (
    <DashboardLayout>
      <div className="space-y-6">
        {/* Header */}
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Рабочие часы</h1>
            <p className="text-gray-600">
              Управление рабочими часами автосервиса
            </p>
          </div>
          <Button className="flex items-center gap-2">
            <PlusIcon className="h-4 w-4" />
            Добавить рабочие часы
          </Button>
        </div>

        {/* Working Hours Table */}
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    День недели
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Время работы
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
                {workingHours.map((workingHour: WorkingHour) => (
                  <tr key={workingHour.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <div className="flex-shrink-0 h-10 w-10">
                          <div className="h-10 w-10 rounded-full bg-gray-300 flex items-center justify-center">
                            <ClockIcon className="h-5 w-5 text-gray-600" />
                          </div>
                        </div>
                        <div className="ml-4">
                          <div className="text-sm font-medium text-gray-900">
                            {dayNames[workingHour.dayOfWeek]}
                          </div>
                        </div>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      {editingId === workingHour.id ? (
                        <div className="flex items-center space-x-2">
                          <input
                            type="time"
                            value={workingHour.openTime}
                            onChange={(e) => handleSave(workingHour.id, { openTime: e.target.value })}
                            className="px-2 py-1 border border-gray-300 rounded text-sm"
                          />
                          <span className="text-gray-500">-</span>
                          <input
                            type="time"
                            value={workingHour.closeTime}
                            onChange={(e) => handleSave(workingHour.id, { closeTime: e.target.value })}
                            className="px-2 py-1 border border-gray-300 rounded text-sm"
                          />
                        </div>
                      ) : (
                        <div className="text-sm text-gray-900">
                          {workingHour.isOpen 
                            ? `${workingHour.openTime} - ${workingHour.closeTime}`
                            : "Закрыто"
                          }
                        </div>
                      )}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      {editingId === workingHour.id ? (
                        <select
                          value={workingHour.isOpen ? "open" : "closed"}
                          onChange={(e) => handleSave(workingHour.id, { isOpen: e.target.value === "open" })}
                          className="px-2 py-1 border border-gray-300 rounded text-sm"
                        >
                          <option value="open">Открыто</option>
                          <option value="closed">Закрыто</option>
                        </select>
                      ) : (
                        <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(workingHour.isOpen)}`}>
                          {getStatusText(workingHour.isOpen)}
                        </span>
                      )}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                      <div className="flex space-x-2">
                        {editingId === workingHour.id ? (
                          <>
                            <button 
                              onClick={() => setEditingId(null)}
                              className="text-green-600 hover:text-green-900"
                            >
                              Сохранить
                            </button>
                            <button 
                              onClick={() => setEditingId(null)}
                              className="text-gray-600 hover:text-gray-900"
                            >
                              Отмена
                            </button>
                          </>
                        ) : (
                          <>
                            <button 
                              onClick={() => setEditingId(workingHour.id)}
                              className="text-indigo-600 hover:text-indigo-900"
                            >
                              <PencilIcon className="h-4 w-4" />
                            </button>
                            <button 
                              onClick={() => handleDelete(workingHour.id)}
                              className="text-red-600 hover:text-red-900"
                            >
                              <TrashIcon className="h-4 w-4" />
                            </button>
                          </>
                        )}
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>

        {/* Summary */}
        <div className="bg-white rounded-lg shadow p-6">
          <h3 className="text-lg font-medium text-gray-900 mb-4">Сводка рабочих часов</h3>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <div className="bg-blue-50 p-4 rounded-lg">
              <div className="text-sm font-medium text-blue-800">Рабочих дней</div>
              <div className="text-2xl font-bold text-blue-900">
                {workingHours.filter(wh => wh.isOpen).length}
              </div>
            </div>
            <div className="bg-green-50 p-4 rounded-lg">
              <div className="text-sm font-medium text-green-800">Выходных дней</div>
              <div className="text-2xl font-bold text-green-900">
                {workingHours.filter(wh => !wh.isOpen).length}
              </div>
            </div>
            <div className="bg-purple-50 p-4 rounded-lg">
              <div className="text-sm font-medium text-purple-800">Средний рабочий день</div>
              <div className="text-2xl font-bold text-purple-900">
                {(() => {
                  const openDays = workingHours.filter(wh => wh.isOpen);
                  if (openDays.length === 0) return "0ч";
                  
                  const totalMinutes = openDays.reduce((total, wh) => {
                    const open = new Date(`2000-01-01T${wh.openTime}`);
                    const close = new Date(`2000-01-01T${wh.closeTime}`);
                    const diff = (close.getTime() - open.getTime()) / (1000 * 60);
                    return total + diff;
                  }, 0);
                  
                  const avgHours = Math.round(totalMinutes / openDays.length / 60);
                  return `${avgHours}ч`;
                })()}
              </div>
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
} 