import axios from "axios";

export const httpClient = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

export const handleApiError = (e: unknown) => {
  if (axios.isAxiosError(e)) {
    throw new Error(e.response?.data?.message || "Server error");
  } else if (e instanceof Error) {
    throw new Error(e.message);
  } else {
    throw new Error("An unknown error occurred");
  }
};
