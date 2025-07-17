# Интеграция Frontend с Backend

## 🔗 Подключение к API

### 1. Настройка переменных окружения

Создайте файл `.env.local` в корне проекта:

```env
# API Configuration
NEXT_PUBLIC_API_URL=http://localhost:7033

# Authentication
NEXT_PUBLIC_AUTH_ENABLED=true

# Development
NODE_ENV=development
```

### 2. Конфигурация API клиента

API клиент настроен в `src/lib/api.ts`:

```typescript
export const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || "http://localhost:7033",
  headers: {
    "Content-Type": "application/json",
  },
});
```

### 3. Автоматическое добавление токена

```typescript
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
```

### 4. Обработка ошибок аутентификации

```typescript
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
```

## 🔐 Аутентификация

### Логин

```typescript
const login = async (email: string, password: string) => {
  const response = await authAPI.login({ email, password });
  localStorage.setItem("token", response.token);
  setUser(response.user);
  router.push("/dashboard");
};
```

### Проверка текущего пользователя

```typescript
const checkAuth = async () => {
  const token = localStorage.getItem("token");
  if (token) {
    const userData = await authAPI.getCurrentUser();
    setUser(userData);
  }
};
```

### Логаут

```typescript
const logout = () => {
  localStorage.removeItem("token");
  setUser(null);
  router.push("/login");
};
```

## 📊 Работа с данными

### Использование React Query

```typescript
// Получение данных
const { data: users, isLoading, error } = useUsers(page, pageSize);

// Мутации
const createUserMutation = useCreateUser();
const updateUserMutation = useUpdateUser();
const deleteUserMutation = useDeleteUser();
```

### Обработка ошибок

```typescript
const createUserMutation = useCreateUser();
const updateUserMutation = useUpdateUser();

// Автоматические уведомления при успехе/ошибке
const mutation = useMutation({
  mutationFn: usersAPI.create,
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: ["users"] });
    toast.success("Пользователь успешно создан");
  },
  onError: (error: any) => {
    toast.error(error.response?.data?.message || "Ошибка создания пользователя");
  },
});
```

## 🎯 Основные API эндпоинты

### Аутентификация

```typescript
// POST /api/auth/login
interface LoginRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
  user: User;
}

// GET /api/current-user
interface User {
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
```

### Пользователи

```typescript
// GET /api/users?page=1&pageSize=10
interface PaginatedResponse<T> {
  data: T[];
  totalItems: number;
  totalPages: number;
  currentPage: number;
  pageSize: number;
}

// POST /api/users
// PUT /api/users/{id}
// DELETE /api/users/{id}
// PATCH /api/users/{id}/toggle-status
```

### Клиенты

```typescript
interface Customer {
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

// GET /api/customers?page=1&pageSize=10
// POST /api/customers
// PUT /api/customers/{id}
// DELETE /api/customers/{id}
// PATCH /api/customers/{id}/toggle-status
```

### Автомобили

```typescript
interface Car {
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

// GET /api/cars?page=1&pageSize=10
// GET /api/cars/owner/{ownerId}
// POST /api/cars
// PUT /api/cars/{id}
// DELETE /api/cars/{id}
```

### Заказы на ремонт

```typescript
interface RepairOrder {
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

// GET /api/repair-orders?page=1&pageSize=10
// GET /api/repair-orders/customer/{customerId}
// POST /api/repair-orders
// PUT /api/repair-orders/{id}
// PATCH /api/repair-orders/{id}/status
// DELETE /api/repair-orders/{id}
```

### Дашборд

```typescript
interface DashboardStats {
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

// GET /api/dashboard/stats
// GET /api/dashboard/recent-orders?limit=10
```

## 🔄 Batch API

Для выполнения множественных операций:

```typescript
// POST /api/b
interface BatchRequest {
  requests: Array<{
    method: string;
    url: string;
    data?: any;
  }>;
}

const batchAPI = {
  execute: async (requests: any[]): Promise<any[]> => {
    const response = await api.post("/api/b", { requests });
    return response.data;
  }
};
```

## 🛡️ Безопасность

### CSRF Protection

Бэкенд использует CSRF токены. Фронтенд автоматически отправляет их:

```typescript
// Автоматически добавляется в заголовки
api.interceptors.request.use((config) => {
  const csrfToken = document.querySelector('meta[name="csrf-token"]')?.getAttribute('content');
  if (csrfToken) {
    config.headers['X-CSRF-TOKEN'] = csrfToken;
  }
  return config;
});
```

### Валидация данных

Все формы используют React Hook Form с валидацией:

```typescript
const form = useForm<CreateUserForm>({
  resolver: zodResolver(createUserSchema),
  defaultValues: {
    name: "",
    email: "",
    role: "customer",
  },
});
```

## 🎨 UI Компоненты

### Кнопки

```typescript
import { Button } from "@/components/ui/button";

<Button variant="default" size="sm">
  Сохранить
</Button>

<Button variant="outline" size="sm">
  Отмена
</Button>

<Button variant="destructive" size="sm">
  Удалить
</Button>
```

### Уведомления

```typescript
import { toast } from "react-hot-toast";

toast.success("Операция выполнена успешно");
toast.error("Произошла ошибка");
toast.loading("Загрузка...");
```

### Модальные окна

```typescript
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";

<Dialog open={isOpen} onOpenChange={setIsOpen}>
  <DialogContent>
    <DialogHeader>
      <DialogTitle>Заголовок</DialogTitle>
    </DialogHeader>
    {/* Содержимое */}
  </DialogContent>
</Dialog>
```

## 📱 Responsive Design

Все компоненты адаптивны:

```css
/* Мобильные устройства */
@media (max-width: 768px) {
  .table-container {
    overflow-x: auto;
  }
}

/* Планшеты */
@media (min-width: 769px) and (max-width: 1024px) {
  .grid {
    grid-template-columns: repeat(2, 1fr);
  }
}

/* Десктоп */
@media (min-width: 1025px) {
  .grid {
    grid-template-columns: repeat(3, 1fr);
  }
}
```

## 🚀 Оптимизация производительности

### Кэширование

React Query автоматически кэширует данные:

```typescript
const { data } = useQuery({
  queryKey: ["users", page, pageSize],
  queryFn: () => usersAPI.getAll(page, pageSize),
  staleTime: 5 * 60 * 1000, // 5 минут
  cacheTime: 10 * 60 * 1000, // 10 минут
});
```

### Ленивая загрузка

```typescript
import dynamic from 'next/dynamic';

const UserModal = dynamic(() => import('@/components/UserModal'), {
  loading: () => <div>Загрузка...</div>,
  ssr: false,
});
```

### Оптимизация изображений

```typescript
import Image from 'next/image';

<Image
  src="/avatar.jpg"
  alt="Avatar"
  width={40}
  height={40}
  className="rounded-full"
/>
```

## 🔧 Отладка

### Логирование

```typescript
// В режиме разработки
if (process.env.NODE_ENV === 'development') {
  console.log('API Response:', response.data);
}
```

### React Query DevTools

```typescript
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';

function App() {
  return (
    <>
      {/* Ваше приложение */}
      <ReactQueryDevtools initialIsOpen={false} />
    </>
  );
}
```

## 📋 Чек-лист интеграции

- [ ] Настроены переменные окружения
- [ ] API клиент настроен и протестирован
- [ ] Аутентификация работает
- [ ] Все эндпоинты протестированы
- [ ] Обработка ошибок настроена
- [ ] Валидация форм работает
- [ ] Responsive дизайн протестирован
- [ ] Производительность оптимизирована
- [ ] Безопасность настроена
- [ ] Документация обновлена 