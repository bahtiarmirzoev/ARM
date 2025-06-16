"use client";

import { Button } from "@/app/presentation/components/ui/button";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/app/presentation/components/ui/card";
import { Badge } from "@/app/presentation/components/ui/badge";
import {
  Star,
  MapPin,
  Phone,
  Mail,
  Clock,
  Users,
  Shield,
  ArrowRight,
} from "lucide-react";
import { useIntl } from "react-intl";

interface Partner {
  id: number;
  name: string;
  location: string;
  phone: string;
  email: string;
  rating: number;
  reviews: number;
  clients: string;
  services: string[];
  workingHours: string;
  experience: string;
  specialization: string;
  certified: boolean;
  premium: boolean;
}

export default function PartnersPage() {
  const intl = useIntl();

  const partners: Partner[] = [
    {
      id: 1,
      name: "Avto Service Pro",
      location: "Bakı, Nərimanov r., Heydər Əliyev pr. 152",
      phone: "+994 12 555 00 11",
      email: "info@autoservice-pro.az",
      rating: 4.9,
      reviews: 234,
      clients: "500+",
      services: [
        intl.formatMessage({ id: "partners.service.diagnostic" }),
        intl.formatMessage({ id: "partners.service.repair" }),
        intl.formatMessage({ id: "partners.service.maintenance" }),
      ],
      workingHours: "B.e-C: 09:00-20:00",
      experience: "10",
      specialization: intl.formatMessage({ id: "partners.service.premium" }),
      certified: true,
      premium: true,
    },
    {
      id: 2,
      name: "Sürətli Təmir",
      location: "Bakı, Yasamal r., Şərifzadə küç. 78",
      phone: "+994 12 555 00 22",
      email: "info@suretli-temir.az",
      rating: 4.8,
      reviews: 189,
      clients: "300+",
      services: [
        intl.formatMessage({ id: "partners.service.express" }),
        intl.formatMessage({ id: "partners.service.maintenance" }),
      ],
      workingHours: "B.e-C: 08:00-22:00",
      experience: "5",
      specialization: intl.formatMessage({ id: "partners.service.express" }),
      certified: true,
      premium: false,
    },
    {
      id: 3,
      name: "Master Avto",
      location: "Sumqayıt, Sülh küç. 45",
      phone: "+994 12 555 00 33",
      email: "info@master-avto.az",
      rating: 4.9,
      reviews: 156,
      clients: "400+",
      services: [
        intl.formatMessage({ id: "partners.service.electronics" }),
        intl.formatMessage({ id: "partners.service.diagnostic" }),
      ],
      workingHours: "B.e-Ş: 09:00-19:00",
      experience: "8",
      specialization: intl.formatMessage({
        id: "partners.service.electronics",
      }),
      certified: true,
      premium: true,
    },
    {
      id: 4,
      name: "Premium Avto Servis",
      location: "Gəncə, Atatürk pr. 89",
      phone: "+994 12 555 00 44",
      email: "info@premium-avto.az",
      rating: 5.0,
      reviews: 98,
      clients: "200+",
      services: [
        intl.formatMessage({ id: "partners.service.premium" }),
        intl.formatMessage({ id: "partners.service.body" }),
        intl.formatMessage({ id: "partners.service.painting" }),
      ],
      workingHours: "B.e-C: 09:00-20:00",
      experience: "12",
      specialization: intl.formatMessage({ id: "partners.service.premium" }),
      certified: true,
      premium: true,
    },
  ];

  return (
    <div className="min-h-screen bg-gradient-to-b from-slate-50 to-white pt-32">
      <div className="container mx-auto px-4">
        <div className="max-w-4xl mx-auto">
          <div className="text-center mb-16">
            <h1 className="text-4xl font-bold mb-4">
              {intl.formatMessage({ id: "partners.title" })}
            </h1>
            <p className="text-lg text-muted-foreground">
              {intl.formatMessage({ id: "partners.subtitle" })}
            </p>
          </div>

          <div className="grid md:grid-cols-2 gap-8">
            {partners.map((partner) => (
              <Card
                key={partner.id}
                className="border-0 shadow-lg hover:shadow-xl transition-shadow duration-300"
              >
                <CardHeader>
                  <div className="flex items-center justify-between mb-4">
                    <CardTitle className="text-xl">{partner.name}</CardTitle>
                    <div className="flex items-center">
                      {partner.premium && (
                        <Badge variant="secondary" className="mr-2">
                          {intl.formatMessage({
                            id: "partners.status.premium",
                          })}
                        </Badge>
                      )}
                      {partner.certified && (
                        <Badge
                          variant="outline"
                          className="text-green-600 border-green-200"
                        >
                          <Shield className="h-3 w-3 mr-1" />
                          {intl.formatMessage({
                            id: "partners.status.certified",
                          })}
                        </Badge>
                      )}
                    </div>
                  </div>
                  <div className="flex items-center justify-between">
                    <div className="flex items-center text-yellow-500">
                      <Star className="h-5 w-5 fill-current" />
                      <span className="ml-1 font-medium">{partner.rating}</span>
                      <span className="ml-1 text-muted-foreground">
                        ({partner.reviews}{" "}
                        {intl.formatMessage({ id: "partners.reviews" })})
                      </span>
                    </div>
                  </div>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    <div className="flex items-center text-muted-foreground">
                      <MapPin className="h-4 w-4 mr-2 shrink-0" />
                      <span>{partner.location}</span>
                    </div>
                    <div className="flex items-center text-muted-foreground">
                      <Clock className="h-4 w-4 mr-2 shrink-0" />
                      <span>
                        {intl.formatMessage({ id: "partners.workingHours" })}:{" "}
                        {partner.workingHours}
                      </span>
                    </div>
                    <div className="flex items-center text-muted-foreground">
                      <Users className="h-4 w-4 mr-2 shrink-0" />
                      <span>
                        {partner.clients}{" "}
                        {intl.formatMessage({ id: "partners.clients" })}
                      </span>
                    </div>
                    <div>
                      <div className="text-sm font-medium mb-2">
                        {intl.formatMessage({ id: "partners.services" })}:
                      </div>
                      <div className="flex flex-wrap gap-2">
                        {partner.services.map((service, idx) => (
                          <Badge key={idx} variant="secondary">
                            {service}
                          </Badge>
                        ))}
                      </div>
                    </div>
                    <div className="flex justify-between items-center pt-4 border-t">
                      <div className="flex gap-2">
                        <Button variant="outline" size="sm">
                          <Phone className="h-4 w-4 mr-1" />
                          {intl.formatMessage({ id: "partners.call" })}
                        </Button>
                        <Button variant="outline" size="sm">
                          <Mail className="h-4 w-4 mr-1" />
                          {intl.formatMessage({ id: "partners.email" })}
                        </Button>
                      </div>
                      <Button
                        size="sm"
                        className="bg-blue-600 hover:bg-blue-700"
                      >
                        {intl.formatMessage({ id: "partners.details" })}
                        <ArrowRight className="h-4 w-4 ml-1" />
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
