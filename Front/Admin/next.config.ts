import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  experimental: {
    optimizePackageImports: ["@heroicons/react", "lucide-react"],
  },
  images: {
    domains: ["localhost"],
  },
};

export default nextConfig; 