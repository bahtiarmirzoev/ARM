import axios, { AxiosResponse } from "axios";
import { 
  User, 
  Customer, 
  Car, 
  RepairOrder, 
  Brand, 
  Service, 
  Review,
  LoginRequest,
  LoginResponse,
  ApiResponse,
  PaginatedResponse,
  DashboardStats
} from "@/types";

// Создание axios инстанса
export const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || "http://localhost:7033",
  headers: {
    "Content-Type": "application/json",
  },
});

// Request interceptor для добавления токена
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor для обработки ошибок
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem("token");
      window.location.href = "/login";
    }
    return Promise.reject(error);
  }
);

// Auth API
export const authAPI = {
  login: async (credentials: LoginRequest): Promise<LoginResponse> => {
    const response: AxiosResponse<LoginResponse> = await api.post("/api/auth/login", credentials);
    return response.data;
  },

  getCurrentUser: async (): Promise<User> => {
    const response: AxiosResponse<User> = await api.get("/api/current-user");
    return response.data;
  },

  logout: async (): Promise<void> => {
    await api.post("/api/auth/logout");
  }
};

// Users API
export const usersAPI = {
  getAll: async (page = 1, pageSize = 10): Promise<PaginatedResponse<User>> => {
    const response: AxiosResponse<PaginatedResponse<User>> = await api.get(`/api/users?page=${page}&pageSize=${pageSize}`);
    return response.data;
  },

  getById: async (id: string): Promise<User> => {
    const response: AxiosResponse<User> = await api.get(`/api/users/${id}`);
    return response.data;
  },

  create: async (userData: Partial<User>): Promise<User> => {
    const response: AxiosResponse<User> = await api.post("/api/users", userData);
    return response.data;
  },

  update: async (id: string, userData: Partial<User>): Promise<User> => {
    const response: AxiosResponse<User> = await api.put(`/api/users/${id}`, userData);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/api/users/${id}`);
  },

  toggleStatus: async (id: string): Promise<User> => {
    const response: AxiosResponse<User> = await api.patch(`/api/users/${id}/toggle-status`);
    return response.data;
  }
};

// Customers API
export const customersAPI = {
  getAll: async (page = 1, pageSize = 10): Promise<PaginatedResponse<Customer>> => {
    const response: AxiosResponse<PaginatedResponse<Customer>> = await api.get(`/api/customers?page=${page}&pageSize=${pageSize}`);
    return response.data;
  },

  getById: async (id: string): Promise<Customer> => {
    const response: AxiosResponse<Customer> = await api.get(`/api/customers/${id}`);
    return response.data;
  },

  create: async (customerData: Partial<Customer>): Promise<Customer> => {
    const response: AxiosResponse<Customer> = await api.post("/api/customers", customerData);
    return response.data;
  },

  update: async (id: string, customerData: Partial<Customer>): Promise<Customer> => {
    const response: AxiosResponse<Customer> = await api.put(`/api/customers/${id}`, customerData);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/api/customers/${id}`);
  },

  toggleStatus: async (id: string): Promise<Customer> => {
    const response: AxiosResponse<Customer> = await api.patch(`/api/customers/${id}/toggle-status`);
    return response.data;
  }
};

// Cars API
export const carsAPI = {
  getAll: async (page = 1, pageSize = 10): Promise<PaginatedResponse<Car>> => {
    const response: AxiosResponse<PaginatedResponse<Car>> = await api.get(`/api/cars?page=${page}&pageSize=${pageSize}`);
    return response.data;
  },

  getById: async (id: string): Promise<Car> => {
    const response: AxiosResponse<Car> = await api.get(`/api/cars/${id}`);
    return response.data;
  },

  getByOwner: async (ownerId: string): Promise<Car[]> => {
    const response: AxiosResponse<Car[]> = await api.get(`/api/cars/owner/${ownerId}`);
    return response.data;
  },

  create: async (carData: Partial<Car>): Promise<Car> => {
    const response: AxiosResponse<Car> = await api.post("/api/cars", carData);
    return response.data;
  },

  update: async (id: string, carData: Partial<Car>): Promise<Car> => {
    const response: AxiosResponse<Car> = await api.put(`/api/cars/${id}`, carData);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/api/cars/${id}`);
  }
};

// Repair Orders API
export const repairOrdersAPI = {
  getAll: async (page = 1, pageSize = 10): Promise<PaginatedResponse<RepairOrder>> => {
    const response: AxiosResponse<PaginatedResponse<RepairOrder>> = await api.get(`/api/repair-orders?page=${page}&pageSize=${pageSize}`);
    return response.data;
  },

  getById: async (id: string): Promise<RepairOrder> => {
    const response: AxiosResponse<RepairOrder> = await api.get(`/api/repair-orders/${id}`);
    return response.data;
  },

  getByCustomer: async (customerId: string): Promise<RepairOrder[]> => {
    const response: AxiosResponse<RepairOrder[]> = await api.get(`/api/repair-orders/customer/${customerId}`);
    return response.data;
  },

  create: async (orderData: Partial<RepairOrder>): Promise<RepairOrder> => {
    const response: AxiosResponse<RepairOrder> = await api.post("/api/repair-orders", orderData);
    return response.data;
  },

  update: async (id: string, orderData: Partial<RepairOrder>): Promise<RepairOrder> => {
    const response: AxiosResponse<RepairOrder> = await api.put(`/api/repair-orders/${id}`, orderData);
    return response.data;
  },

  updateStatus: async (id: string, status: string): Promise<RepairOrder> => {
    const response: AxiosResponse<RepairOrder> = await api.patch(`/api/repair-orders/${id}/status`, { status });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/api/repair-orders/${id}`);
  }
};

// Brands API
export const brandsAPI = {
  getAll: async (): Promise<Brand[]> => {
    const response: AxiosResponse<Brand[]> = await api.get("/api/brands");
    return response.data;
  },

  getById: async (id: string): Promise<Brand> => {
    const response: AxiosResponse<Brand> = await api.get(`/api/brands/${id}`);
    return response.data;
  },

  create: async (brandData: Partial<Brand>): Promise<Brand> => {
    const response: AxiosResponse<Brand> = await api.post("/api/brands", brandData);
    return response.data;
  },

  update: async (id: string, brandData: Partial<Brand>): Promise<Brand> => {
    const response: AxiosResponse<Brand> = await api.put(`/api/brands/${id}`, brandData);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/api/brands/${id}`);
  }
};

// Services API
export const servicesAPI = {
  getAll: async (): Promise<Service[]> => {
    const response: AxiosResponse<Service[]> = await api.get("/api/services");
    return response.data;
  },

  getByBrand: async (brandId: string): Promise<Service[]> => {
    const response: AxiosResponse<Service[]> = await api.get(`/api/services/brand/${brandId}`);
    return response.data;
  },

  create: async (serviceData: Partial<Service>): Promise<Service> => {
    const response: AxiosResponse<Service> = await api.post("/api/services", serviceData);
    return response.data;
  },

  update: async (id: string, serviceData: Partial<Service>): Promise<Service> => {
    const response: AxiosResponse<Service> = await api.put(`/api/services/${id}`, serviceData);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/api/services/${id}`);
  }
};

// Reviews API
export const reviewsAPI = {
  getAll: async (page = 1, pageSize = 10): Promise<PaginatedResponse<Review>> => {
    const response: AxiosResponse<PaginatedResponse<Review>> = await api.get(`/api/reviews?page=${page}&pageSize=${pageSize}`);
    return response.data;
  },

  getByBrand: async (brandId: string): Promise<Review[]> => {
    const response: AxiosResponse<Review[]> = await api.get(`/api/reviews/brand/${brandId}`);
    return response.data;
  },

  create: async (reviewData: Partial<Review>): Promise<Review> => {
    const response: AxiosResponse<Review> = await api.post("/api/reviews", reviewData);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/api/reviews/${id}`);
  }
};

// Dashboard API
export const dashboardAPI = {
  getStats: async (): Promise<DashboardStats> => {
    const response: AxiosResponse<DashboardStats> = await api.get("/api/dashboard/stats");
    return response.data;
  },

  getRecentOrders: async (limit = 10): Promise<RepairOrder[]> => {
    const response: AxiosResponse<RepairOrder[]> = await api.get(`/api/dashboard/recent-orders?limit=${limit}`);
    return response.data;
  }
};

// Batch API для выполнения множественных операций
export const batchAPI = {
  execute: async (requests: any[]): Promise<any[]> => {
    const response: AxiosResponse<any[]> = await api.post("/api/b", { requests });
    return response.data;
  }
}; 