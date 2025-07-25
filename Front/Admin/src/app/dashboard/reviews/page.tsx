"use client";

import { useState } from "react";
import { DashboardLayout } from "@/components/layout/dashboard-layout";
import { Button } from "@/components/ui/button";
import { Review } from "@/types";
import {
  PlusIcon,
  MagnifyingGlassIcon,
  FunnelIcon,
  EyeIcon,
  PencilIcon,
  TrashIcon,
  StarIcon,
} from "@heroicons/react/24/outline";
import toast from "react-hot-toast";

export default function ReviewsPage() {
  const [page, setPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedRating, setSelectedRating] = useState("all");
  const [selectedBrand, setSelectedBrand] = useState("all");

  // Мок данные отзывов
  const mockReviews: Review[] = [
    {
      id: "1",
      customerId: "1",
      customerName: "Иван Петров",
      brandId: "1",
      brandName: "Автосервис Центральный",
      rating: 5,
      comment: "Отличный сервис! Быстро и качественно выполнили замену масла. Персонал вежливый и профессиональный.",
      createdAt: "2024-01-15",
    },
    {
      id: "2",
      customerId: "2",
      customerName: "Мария Сидорова",
      brandId: "1",
      brandName: "Автосервис Центральный",
      rating: 4,
      comment: "Хороший сервис, но немного долго ждала. В целом довольна качеством работы.",
      createdAt: "2024-01-14",
    },
    {
      id: "3",
      customerId: "3",
      customerName: "Алексей Козлов",
      brandId: "1",
      brandName: "Автосервис Центральный",
      rating: 5,
      comment: "Профессиональная диагностика двигателя. Все объяснили доступно, рекомендую!",
      createdAt: "2024-01-13",
    },
    {
      id: "4",
      customerId: "4",
      customerName: "Елена Волкова",
      brandId: "1",
      brandName: "Автосервис Центральный",
      rating: 3,
      comment: "Сервис нормальный, но цены немного завышены. Качество работы удовлетворительное.",
      createdAt: "2024-01-12",
    },
    {
      id: "5",
      customerId: "5",
      customerName: "Дмитрий Соколов",
      brandId: "1",
      brandName: "Автосервис Центральный",
      rating: 5,
      comment: "Лучший автосервис в городе! Быстро, качественно, недорого. Буду обращаться еще.",
      createdAt: "2024-01-11",
    },
  ];

  const reviews = mockReviews;
  const totalPages = 1;

  const handleDeleteReview = async (reviewId: string) => {
    if (window.confirm("Вы уверены, что хотите удалить этот отзыв?")) {
      toast.success("Отзыв успешно удален");
    }
  };

  const getRatingColor = (rating: number) => {
    if (rating >= 4) return "text-green-600";
    if (rating >= 3) return "text-yellow-600";
    return "text-red-600";
  };

  const getRatingText = (rating: number) => {
    if (rating === 5) return "Отлично";
    if (rating === 4) return "Хорошо";
    if (rating === 3) return "Удовлетворительно";
    if (rating === 2) return "Плохо";
    return "Очень плохо";
  };

  const renderStars = (rating: number) => {
    return Array.from({ length: 5 }, (_, i) => (
      <StarIcon
        key={i}
        className={`h-4 w-4 ${
          i < rating ? "text-yellow-400 fill-current" : "text-gray-300"
        }`}
      />
    ));
  };

  const filteredReviews = reviews.filter((review: Review) => {
    const matchesSearch = review.customerName.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         review.comment.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         review.brandName.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesRating = selectedRating === "all" || review.rating.toString() === selectedRating;
    const matchesBrand = selectedBrand === "all" || review.brandId === selectedBrand;
    
    return matchesSearch && matchesRating && matchesBrand;
  });

  const brands = Array.from(new Set(reviews.map(r => ({ id: r.brandId, name: r.brandName }))));
  const averageRating = reviews.length > 0 
    ? (reviews.reduce((sum, r) => sum + r.rating, 0) / reviews.length).toFixed(1)
    : "0.0";

  return (
    <DashboardLayout>
      <div className="space-y-6">
        {/* Header */}
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Отзывы</h1>
            <p className="text-gray-600">
              Управление отзывами клиентов
            </p>
          </div>
          <Button className="flex items-center gap-2">
            <PlusIcon className="h-4 w-4" />
            Добавить отзыв
          </Button>
        </div>

        {/* Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <StarIcon className="h-8 w-8 text-yellow-400" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Средний рейтинг
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {averageRating}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <StarIcon className="h-8 w-8 text-green-400" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Отличных отзывов
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {reviews.filter(r => r.rating === 5).length}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <StarIcon className="h-8 w-8 text-yellow-400" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Хороших отзывов
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {reviews.filter(r => r.rating === 4).length}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-white rounded-lg shadow p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <StarIcon className="h-8 w-8 text-red-400" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Плохих отзывов
                  </dt>
                  <dd className="text-2xl font-semibold text-gray-900">
                    {reviews.filter(r => r.rating <= 3).length}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        {/* Filters */}
        <div className="bg-white rounded-lg shadow p-6">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
            {/* Search */}
            <div className="relative">
              <MagnifyingGlassIcon className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
              <input
                type="text"
                placeholder="Поиск по клиенту или комментарию..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="pl-10 pr-4 py-2 w-full border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              />
            </div>

            {/* Rating Filter */}
            <select
              value={selectedRating}
              onChange={(e) => setSelectedRating(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            >
              <option value="all">Все рейтинги</option>
              <option value="5">5 звезд</option>
              <option value="4">4 звезды</option>
              <option value="3">3 звезды</option>
              <option value="2">2 звезды</option>
              <option value="1">1 звезда</option>
            </select>

            {/* Brand Filter */}
            <select
              value={selectedBrand}
              onChange={(e) => setSelectedBrand(e.target.value)}
              className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            >
              <option value="all">Все автосервисы</option>
              {brands.map((brand) => (
                <option key={brand.id} value={brand.id}>{brand.name}</option>
              ))}
            </select>

            {/* Clear Filters */}
            <Button
              onClick={() => {
                setSearchTerm("");
                setSelectedRating("all");
                setSelectedBrand("all");
              }}
              className="flex items-center gap-2"
            >
              <FunnelIcon className="h-4 w-4" />
              Сбросить
            </Button>
          </div>
        </div>

        {/* Reviews Table */}
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Клиент
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Автосервис
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Рейтинг
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Комментарий
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Дата
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Действия
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {filteredReviews.map((review: Review) => (
                  <tr key={review.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <div className="flex-shrink-0 h-10 w-10">
                          <div className="h-10 w-10 rounded-full bg-gray-300 flex items-center justify-center">
                            <span className="text-sm font-medium text-gray-700">
                              {review.customerName.charAt(0).toUpperCase()}
                            </span>
                          </div>
                        </div>
                        <div className="ml-4">
                          <div className="text-sm font-medium text-gray-900">
                            {review.customerName}
                          </div>
                        </div>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                      {review.brandName}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex items-center">
                        <div className="flex">
                          {renderStars(review.rating)}
                        </div>
                        <span className={`ml-2 text-sm font-medium ${getRatingColor(review.rating)}`}>
                          {getRatingText(review.rating)}
                        </span>
                      </div>
                    </td>
                    <td className="px-6 py-4">
                      <div className="text-sm text-gray-900 max-w-xs truncate">
                        {review.comment}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      {new Date(review.createdAt).toLocaleDateString('ru-RU')}
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
                          onClick={() => handleDeleteReview(review.id)}
                          className="text-red-600 hover:text-red-900"
                        >
                          <TrashIcon className="h-4 w-4" />
                        </button>
                      </div>
                    </td>
                  </tr>
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
                    <span className="font-medium">{Math.min(page * 10, filteredReviews.length)}</span> из{" "}
                    <span className="font-medium">{filteredReviews.length}</span> результатов
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