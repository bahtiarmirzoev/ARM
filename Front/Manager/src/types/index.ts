export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  phone: string;
  role: 'manager' | 'admin' | 'super-admin';
  isActive: boolean;
  createdAt: string;
}

export interface Customer {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  phone: string;
  isActive: boolean;
  createdAt: string;
}

export interface Car {
  id: string;
  brand: string;
  model: string;
  year: number;
  licensePlate: string;
  vin: string;
  color: string;
  mileage: number;
  customerId: string;
  customerName: string;
  createdAt: string;
}

export interface ServiceRequest {
  id: string;
  title: string;
  description: string;
  status: 'pending' | 'approved' | 'in_progress' | 'completed' | 'cancelled';
  priority: 'low' | 'medium' | 'high';
  estimatedCost: number;
  actualCost?: number;
  carId: string;
  carInfo: string;
  customerId: string;
  customerName: string;
  createdAt: string;
  updatedAt: string;
}

export interface RepairOrder {
  id: string;
  serviceRequestId: string;
  status: 'pending' | 'in_progress' | 'completed' | 'cancelled';
  startDate?: string;
  completionDate?: string;
  totalCost: number;
  notes?: string;
  assignedTo?: string;
  createdAt: string;
  updatedAt: string;
}

export interface Review {
  id: string;
  rating: number;
  comment: string;
  serviceRequestId: string;
  customerId: string;
  customerName: string;
  createdAt: string;
}

export interface WorkingHour {
  id: string;
  dayOfWeek: number;
  dayName: string;
  openTime: string;
  closeTime: string;
  isOpen: boolean;
}

export interface Venue {
  id: string;
  name: string;
  address: string;
  phone: string;
  email: string;
  isActive: boolean;
}

export interface AuthResponse {
  user: User;
  token: string;
  refreshToken: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface DashboardStats {
  totalCustomers: number;
  totalCars: number;
  activeRequests: number;
  completedRequests: number;
  totalRevenue: number;
  monthlyRevenue: number;
} 