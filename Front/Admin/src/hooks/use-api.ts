import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { 
  usersAPI, 
  customersAPI, 
  carsAPI, 
  repairOrdersAPI, 
  brandsAPI, 
  servicesAPI, 
  reviewsAPI, 
  dashboardAPI,
  servicerequestsAPI
} from "@/lib/api";
import { toast } from "react-hot-toast";
import { ServiceRequest, PaginatedResponse } from "@/types";

// Users hooks
export const useUsers = (page = 1, pageSize = 10) => {
  return useQuery({
    queryKey: ["users", page, pageSize],
    queryFn: () => usersAPI.getAll(page, pageSize),
  });
};

export const useUser = (id: string) => {
  return useQuery({
    queryKey: ["user", id],
    queryFn: () => usersAPI.getById(id),
    enabled: !!id,
  });
};

export const useCreateUser = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: usersAPI.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
      toast.success("Пользователь успешно создан");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка создания пользователя");
    },
  });
};

export const useUpdateUser = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: any }) => usersAPI.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
      toast.success("Пользователь успешно обновлен");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка обновления пользователя");
    },
  });
};

export const useDeleteUser = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: usersAPI.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
      toast.success("Пользователь успешно удален");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка удаления пользователя");
    },
  });
};

// Customers hooks
export const useCustomers = (page = 1, pageSize = 10) => {
  return useQuery({
    queryKey: ["customers", page, pageSize],
    queryFn: () => customersAPI.getAll(page, pageSize),
  });
};

export const useCustomer = (id: string) => {
  return useQuery({
    queryKey: ["customer", id],
    queryFn: () => customersAPI.getById(id),
    enabled: !!id,
  });
};

export const useCreateCustomer = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: customersAPI.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["customers"] });
      toast.success("Клиент успешно создан");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка создания клиента");
    },
  });
};

export const useUpdateCustomer = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: any }) => customersAPI.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["customers"] });
      toast.success("Клиент успешно обновлен");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка обновления клиента");
    },
  });
};

export const useDeleteCustomer = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: customersAPI.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["customers"] });
      toast.success("Клиент успешно удален");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка удаления клиента");
    },
  });
};

// Cars hooks
export const useCars = (page = 1, pageSize = 10) => {
  return useQuery({
    queryKey: ["cars", page, pageSize],
    queryFn: () => carsAPI.getAll(page, pageSize),
  });
};

export const useCar = (id: string) => {
  return useQuery({
    queryKey: ["car", id],
    queryFn: () => carsAPI.getById(id),
    enabled: !!id,
  });
};

export const useCarsByOwner = (ownerId: string) => {
  return useQuery({
    queryKey: ["cars", "owner", ownerId],
    queryFn: () => carsAPI.getByOwner(ownerId),
    enabled: !!ownerId,
  });
};

export const useCreateCar = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: carsAPI.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["cars"] });
      toast.success("Автомобиль успешно добавлен");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка добавления автомобиля");
    },
  });
};

export const useUpdateCar = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: any }) => carsAPI.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["cars"] });
      toast.success("Автомобиль успешно обновлен");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка обновления автомобиля");
    },
  });
};

export const useDeleteCar = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: carsAPI.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["cars"] });
      toast.success("Автомобиль успешно удален");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка удаления автомобиля");
    },
  });
};

// Repair Orders hooks
export const useRepairOrders = (page = 1, pageSize = 10) => {
  return useQuery({
    queryKey: ["repair-orders", page, pageSize],
    queryFn: () => repairOrdersAPI.getAll(page, pageSize),
  });
};

export const useRepairOrder = (id: string) => {
  return useQuery({
    queryKey: ["repair-order", id],
    queryFn: () => repairOrdersAPI.getById(id),
    enabled: !!id,
  });
};

export const useRepairOrdersByCustomer = (customerId: string) => {
  return useQuery({
    queryKey: ["repair-orders", "customer", customerId],
    queryFn: () => repairOrdersAPI.getByCustomer(customerId),
    enabled: !!customerId,
  });
};

export const useCreateRepairOrder = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: repairOrdersAPI.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["repair-orders"] });
      toast.success("Заказ на ремонт успешно создан");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка создания заказа");
    },
  });
};

export const useUpdateRepairOrder = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: any }) => repairOrdersAPI.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["repair-orders"] });
      toast.success("Заказ на ремонт успешно обновлен");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка обновления заказа");
    },
  });
};

export const useUpdateRepairOrderStatus = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, status }: { id: string; status: string }) => repairOrdersAPI.updateStatus(id, status),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["repair-orders"] });
      toast.success("Статус заказа обновлен");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка обновления статуса");
    },
  });
};

export const useDeleteRepairOrder = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: repairOrdersAPI.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["repair-orders"] });
      toast.success("Заказ на ремонт успешно удален");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка удаления заказа");
    },
  });
};

// Brands hooks
export const useBrands = () => {
  return useQuery({
    queryKey: ["brands"],
    queryFn: () => brandsAPI.getAll(),
  });
};

export const useBrand = (id: string) => {
  return useQuery({
    queryKey: ["brand", id],
    queryFn: () => brandsAPI.getById(id),
    enabled: !!id,
  });
};

// Services hooks
export const useServices = () => {
  return useQuery({
    queryKey: ["services"],
    queryFn: () => servicesAPI.getAll(),
  });
};

export const useServicesByBrand = (brandId: string) => {
  return useQuery({
    queryKey: ["services", "brand", brandId],
    queryFn: () => servicesAPI.getByBrand(brandId),
    enabled: !!brandId,
  });
};

// Reviews hooks
export const useReviews = (page = 1, pageSize = 10) => {
  return useQuery({
    queryKey: ["reviews", page, pageSize],
    queryFn: () => reviewsAPI.getAll(page, pageSize),
  });
};

export const useReviewsByBrand = (brandId: string) => {
  return useQuery({
    queryKey: ["reviews", "brand", brandId],
    queryFn: () => reviewsAPI.getByBrand(brandId),
    enabled: !!brandId,
  });
};

// Dashboard hooks
export const useDashboardStats = () => {
  return useQuery({
    queryKey: ["dashboard", "stats"],
    queryFn: () => dashboardAPI.getStats(),
  });
};

export const useRecentOrders = (limit = 10) => {
  return useQuery({
    queryKey: ["dashboard", "recent-orders", limit],
    queryFn: () => dashboardAPI.getRecentOrders(limit),
  });
}; 

// Service Requests hooks
export const useServiceRequests = (page = 1, pageSize = 10) => {
  return useQuery<PaginatedResponse<ServiceRequest>>({
    queryKey: ["servicerequests", page, pageSize],
    queryFn: () => servicerequestsAPI.getAll(page, pageSize),
  });
};

export const useServiceRequest = (id: string) => {
  return useQuery<ServiceRequest>({
    queryKey: ["servicerequest", id],
    queryFn: () => servicerequestsAPI.getById(id),
    enabled: !!id,
  });
};

export const useCreateServiceRequest = () => {
  const queryClient = useQueryClient();
  return useMutation<ServiceRequest, any, Partial<ServiceRequest>>({
    mutationFn: servicerequestsAPI.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["servicerequests"] });
      toast.success("Заявка успешно создана");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка создания заявки");
    },
  });
};

export const useUpdateServiceRequest = () => {
  const queryClient = useQueryClient();
  return useMutation<ServiceRequest, any, { id: string; data: Partial<ServiceRequest> }>({
    mutationFn: ({ id, data }: { id: string; data: Partial<ServiceRequest> }) => servicerequestsAPI.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["servicerequests"] });
      toast.success("Заявка успешно обновлена");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка обновления заявки");
    },
  });
};

export const useDeleteServiceRequest = () => {
  const queryClient = useQueryClient();
  return useMutation<void, any, string>({
    mutationFn: servicerequestsAPI.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["servicerequests"] });
      toast.success("Заявка успешно удалена");
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || "Ошибка удаления заявки");
    },
  });
}; 