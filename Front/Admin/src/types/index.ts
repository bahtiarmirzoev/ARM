import React from 'react';

// User types
export interface User {
  id: string;
  name: string;
  email: string;
  role: "admin" | "manager" | "customer";
  isActive: boolean;
  emailVerified: boolean;
  lastLogin?: string;
  createdAt: string;
  permissions: string[];
}

// Customer types
export interface Customer {
  id: string;
  name: string;
  surname: string;
  email: string;
  phoneNumber: string;
  address: string;
  isActive: boolean;
  emailVerified: boolean;
  createdAt: string;
}

// Car types
export interface Car {
  id: string;
  make: string;
  model: string;
  year: number;
  color: string;
  carPlate: string;
  vin: string;
  engineType: string;
  engineVolume: number;
  transmission: string;
  ownerId: string;
  ownerName: string;
  ownerEmail: string;
  createdAt: string;
}

// Repair Order types
export interface RepairOrder {
  id: string;
  customerId: string;
  customerName: string;
  carId: string;
  carInfo: string;
  serviceType: string;
  description: string;
  status: "pending" | "in_progress" | "completed" | "cancelled";
  priority: "low" | "medium" | "high";
  estimatedCost: number;
  actualCost?: number;
  createdAt: string;
  estimatedCompletion?: string;
  completedAt?: string;
}

// Brand types
export interface Brand {
  id: string;
  name: string;
  fullName: string;
  description: string;
  phoneNumber: string;
  address: string;
  rating: number;
  email: string;
  totalReviews: number;
  maxCarsPerDay: number;
  hasParking: boolean;
  hasWaitingRoom: boolean;
  isOpen: boolean;
  createdAt: string;
  updatedAt?: string;
}

// Service types
export interface Service {
  id: string;
  name: string;
  description: string;
  price: number;
  duration: number; // в минутах
  category: string;
  isActive: boolean;
  brandId: string;
  createdAt: string;
}

// Review types
export interface Review {
  id: string;
  customerId: string;
  customerName: string;
  brandId: string;
  brandName: string;
  rating: number;
  comment: string;
  createdAt: string;
}

// Working Hours types
export interface WorkingHour {
  id: string;
  brandId: string;
  dayOfWeek: number; // 0-6 (воскресенье-суббота)
  openTime: string;
  closeTime: string;
  isOpen: boolean;
}

// API Response types
export interface ApiResponse<T> {
  data: T;
  message?: string;
  success: boolean;
}

export interface PaginatedResponse<T> {
  data: T[];
  totalItems: number;
  totalPages: number;
  currentPage: number;
  pageSize: number;
}

// Auth types
export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  user: User;
}

export interface AuthState {
  user: User | null;
  isLoading: boolean;
  isAuthenticated: boolean;
}

// Form types
export interface CreateCustomerForm {
  name: string;
  surname: string;
  email: string;
  phoneNumber: string;
  address: string;
}

export interface CreateCarForm {
  make: string;
  model: string;
  year: number;
  color: string;
  carPlate: string;
  vin: string;
  engineType: string;
  engineVolume: number;
  transmission: string;
  ownerId: string;
}

export interface CreateRepairOrderForm {
  customerId: string;
  carId: string;
  serviceType: string;
  description: string;
  priority: "low" | "medium" | "high";
  estimatedCost: number;
  estimatedCompletion?: string;
}

// Dashboard Stats types
export interface DashboardStats {
  totalCustomers: number;
  totalCars: number;
  totalRepairOrders: number;
  monthlyRevenue: number;
  averageRating: number;
  averageWaitTime: number;
  pendingOrders: number;
  inProgressOrders: number;
  completedOrders: number;
}

// Filter types
export interface FilterOptions {
  search?: string;
  status?: string;
  role?: string;
  priority?: string;
  dateFrom?: string;
  dateTo?: string;
}

// Table types
export interface TableColumn<T> {
  key: keyof T;
  label: string;
  sortable?: boolean;
  render?: (value: any, item: T) => React.ReactNode;
}

export interface SortConfig {
  key: string;
  direction: "asc" | "desc";
}

export interface ServiceRequest {
  id: string;
  name: string;
  phone: string;
  technicalPassport: string;
  make: string;
  model: string;
  problemDescription: string;
  status: string;
  carPlate: string;
  year?: number;
  preferredDate: string;
  email: string;
  requestDate: string;
  autoRepairId: string;
  brandId?: string;
  serviceId?: string;
  userId?: string;
  isProcessed: boolean;
  createdAt: string;
  updatedAt?: string;
} 