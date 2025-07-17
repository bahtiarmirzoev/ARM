export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  phone: string;
  role: 'customer';
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
  customerId: string;
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
  createdAt: string;
  updatedAt: string;
}

export interface Review {
  id: string;
  rating: number;
  comment: string;
  serviceRequestId: string;
  customerId: string;
  createdAt: string;
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

export interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phone: string;
} 