"use client";

import { Button } from "@/app/presentation/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/app/presentation/components/ui/card";
import { Badge } from "@/app/presentation/components/ui/badge";
import {
  Car,
  Users,
  Zap,
  Shield,
  BarChart3,
  Clock,
  ArrowRight,
  Star,
  MapPin,
  Phone,
} from "lucide-react";
import { useIntl } from "react-intl";

export default function HomePage() {
  const intl = useIntl();

  const features = [
    {
      icon: <Car className="h-8 w-8" />,
      titleId: "features.management.title",
      descriptionId: "features.management.description",
    },
    {
      icon: <BarChart3 className="h-8 w-8" />,
      titleId: "features.analytics.title",
      descriptionId: "features.analytics.description",
    },
    {
      icon: <Users className="h-8 w-8" />,
      titleId: "features.crm.title",
      descriptionId: "features.crm.description",
    },
    {
      icon: <Clock className="h-8 w-8" />,
      titleId: "features.booking.title",
      descriptionId: "features.booking.description",
    },
    {
      icon: <Shield className="h-8 w-8" />,
      titleId: "features.security.title",
      descriptionId: "features.security.description",
    },
    {
      icon: <Zap className="h-8 w-8" />,
      titleId: "features.automation.title",
      descriptionId: "features.automation.description",
    },
  ];

  const partners = [
    {
      id: "1",
      name: "AutoService Pro",
      fullName: "AutoService Professional Center",
      description: "Профессиональный автосервис с 15-летним опытом",
      phoneNumber: "+7 (999) 123-45-67",
      address: "ул. Автомобильная, 15",
      averageRating: 4.9,
      totalReviews: 523,
      isOpen: true,
      maxCarsPerDay: 20,
      hasParking: true,
      hasWaitingRoom: true,
      venues: [
        {
          name: "Основной цех",
          address: "ул. Автомобильная, 15",
          phoneNumber: "+7 (999) 123-45-67",
          email: "main@autoservice.pro",
          isOpen: true,
          latitude: 55.7558,
          longitude: 37.6173,
          services: [
            {
              name: "Диагностика двигателя",
              description: "Комплексная диагностика двигателя",
              price: 2000,
              duration: "01:00:00",
              isActive: true,
              rating: 4.9,
            },
            {
              name: "Замена масла",
              description: "Замена масла и фильтров",
              price: 1500,
              duration: "00:30:00",
              isActive: true,
              rating: 4.8,
            },
          ],
        },
      ],
      workingHours: [
        {
          day: "Monday",
          isDayOff: false,
          openTime: "09:00:00",
          closeTime: "20:00:00",
        },
        {
          day: "Sunday",
          isDayOff: true,
          openTime: "00:00:00",
          closeTime: "00:00:00",
        },
      ],
    },
    {
      id: "2",
      name: "CarTech",
      fullName: "CarTech Auto Service",
      description: "Современный автосервис с новейшим оборудованием",
      phoneNumber: "+7 (999) 234-56-78",
      address: "пр. Технический, 42",
      averageRating: 4.8,
      totalReviews: 312,
      isOpen: true,
      maxCarsPerDay: 15,
      hasParking: true,
      hasWaitingRoom: true,
      venues: [
        {
          name: "Технический центр",
          address: "пр. Технический, 42",
          phoneNumber: "+7 (999) 234-56-78",
          email: "service@cartech.ru",
          isOpen: true,
          latitude: 55.7658,
          longitude: 37.6273,
          services: [
            {
              name: "Компьютерная диагностика",
              description: "Диагностика электронных систем",
              price: 2500,
              duration: "01:30:00",
              isActive: true,
              rating: 4.8,
            },
            {
              name: "Ремонт подвески",
              description: "Диагностика и ремонт подвески",
              price: 3000,
              duration: "02:00:00",
              isActive: true,
              rating: 4.7,
            },
          ],
        },
      ],
      workingHours: [
        {
          day: "Monday",
          isDayOff: false,
          openTime: "08:00:00",
          closeTime: "21:00:00",
        },
        {
          day: "Sunday",
          isDayOff: true,
          openTime: "00:00:00",
          closeTime: "00:00:00",
        },
      ],
    },
    {
      id: "3",
      name: "AutoElite",
      fullName: "AutoElite Premium Service",
      description: "Премиальный автосервис для люксовых автомобилей",
      phoneNumber: "+7 (999) 345-67-89",
      address: "ул. Премиум, 7",
      averageRating: 4.9,
      totalReviews: 428,
      isOpen: true,
      maxCarsPerDay: 10,
      hasParking: true,
      hasWaitingRoom: true,
      venues: [
        {
          name: "Премиум-центр",
          address: "ул. Премиум, 7",
          phoneNumber: "+7 (999) 345-67-89",
          email: "premium@autoelite.ru",
          isOpen: true,
          latitude: 55.7758,
          longitude: 37.6373,
          services: [
            {
              name: "Премиум-диагностика",
              description: "Расширенная диагностика премиум-автомобилей",
              price: 5000,
              duration: "02:00:00",
              isActive: true,
              rating: 4.9,
            },
            {
              name: "Обслуживание премиум",
              description: "Комплексное обслуживание премиум-автомобилей",
              price: 8000,
              duration: "03:00:00",
              isActive: true,
              rating: 4.9,
            },
          ],
        },
      ],
      workingHours: [
        {
          day: "Monday",
          isDayOff: false,
          openTime: "10:00:00",
          closeTime: "22:00:00",
        },
        {
          day: "Sunday",
          isDayOff: true,
          openTime: "00:00:00",
          closeTime: "00:00:00",
        },
      ],
    },
    {
      id: "4",
      name: "QuickFix",
      fullName: "QuickFix Express Service",
      description: "Быстрый автосервис для срочного ремонта",
      phoneNumber: "+7 (999) 456-78-90",
      address: "ул. Быстрая, 25",
      averageRating: 4.7,
      totalReviews: 215,
      isOpen: true,
      maxCarsPerDay: 30,
      hasParking: true,
      hasWaitingRoom: true,
      venues: [
        {
          name: "Экспресс-центр",
          address: "ул. Быстрая, 25",
          phoneNumber: "+7 (999) 456-78-90",
          email: "express@quickfix.ru",
          isOpen: true,
          latitude: 55.7858,
          longitude: 37.6473,
          services: [
            {
              name: "Экспресс-диагностика",
              description: "Быстрая диагностика основных систем",
              price: 1500,
              duration: "00:30:00",
              isActive: true,
              rating: 4.7,
            },
            {
              name: "Срочный ремонт",
              description: "Быстрый ремонт неисправностей",
              price: 3000,
              duration: "01:00:00",
              isActive: true,
              rating: 4.6,
            },
          ],
        },
      ],
      workingHours: [
        {
          day: "Monday",
          isDayOff: false,
          openTime: "07:00:00",
          closeTime: "23:00:00",
        },
        {
          day: "Sunday",
          isDayOff: false,
          openTime: "09:00:00",
          closeTime: "21:00:00",
        },
      ],
    },
  ];

  const team = [
    {
      name: intl.formatMessage({ id: "team.member1.name" }),
      role: intl.formatMessage({ id: "team.member1.role" }),
      description: intl.formatMessage({ id: "team.member1.description" }),
    },
    {
      name: intl.formatMessage({ id: "team.member2.name" }),
      role: intl.formatMessage({ id: "team.member2.role" }),
      description: intl.formatMessage({ id: "team.member2.description" }),
    },
    {
      name: intl.formatMessage({ id: "team.member3.name" }),
      role: intl.formatMessage({ id: "team.member3.role" }),
      description: intl.formatMessage({ id: "team.member3.description" }),
    },
    {
      name: intl.formatMessage({ id: "team.member4.name" }),
      role: intl.formatMessage({ id: "team.member4.role" }),
      description: intl.formatMessage({ id: "team.member4.description" }),
    },
  ];

  return (
    <div className="min-h-screen bg-gradient-to-b from-slate-50 to-white">
      {/* Hero Section */}
      <section className="relative py-20 lg:py-32 overflow-hidden">
        <div className="container mx-auto px-4 relative">
          <div className="max-w-4xl mx-auto text-center">
            <Badge
              variant="outline"
              className="mb-6 text-blue-600 border-blue-200"
            >
              {intl.formatMessage({ id: "hero.badge" })}
            </Badge>
            <h1 className="text-4xl lg:text-6xl font-bold mb-6 bg-gradient-to-r from-blue-600 to-purple-600 bg-clip-text text-transparent">
              {intl.formatMessage({ id: "hero.title" })}
            </h1>
            <p className="text-xl text-muted-foreground mb-8 leading-relaxed">
              {intl.formatMessage({ id: "hero.description" })}
            </p>
            <div className="flex flex-col sm:flex-row gap-4 justify-center">
              <Button size="lg" className="bg-blue-600 hover:bg-blue-700">
                {intl.formatMessage({ id: "hero.button.start" })}
                <ArrowRight className="ml-2 h-4 w-4" />
              </Button>
              <Button size="lg" variant="outline">
                {intl.formatMessage({ id: "hero.button.demo" })}
              </Button>
            </div>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className="py-20 bg-slate-50">
        <div className="container mx-auto px-4">
          <div className="text-center mb-16">
            <h2 className="text-3xl lg:text-4xl font-bold mb-4">
              {intl.formatMessage({ id: "features.title" })}
            </h2>
            <p className="text-lg text-muted-foreground max-w-2xl mx-auto">
              {intl.formatMessage({ id: "features.subtitle" })}
            </p>
          </div>
          <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
            {features.map((feature, index) => (
              <Card
                key={index}
                className="border-0 shadow-lg hover:shadow-xl transition-shadow duration-300"
              >
                <CardHeader>
                  <div className="w-16 h-16 bg-blue-100 rounded-lg flex items-center justify-center text-blue-600 mb-4">
                    {feature.icon}
                  </div>
                  <CardTitle className="text-xl">
                    {intl.formatMessage({ id: feature.titleId })}
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <CardDescription className="text-base leading-relaxed">
                    {intl.formatMessage({ id: feature.descriptionId })}
                  </CardDescription>
                </CardContent>
              </Card>
            ))}
          </div>
        </div>
      </section>

      {/* Mission & Vision */}
      <section className="py-20 bg-slate-50">
        <div className="container mx-auto px-4">
          <div className="text-center mb-16">
            <h2 className="text-3xl lg:text-4xl font-bold mb-4">
              {intl.formatMessage({ id: "partners.title" })}
            </h2>
            <p className="text-lg text-muted-foreground max-w-2xl mx-auto">
              {intl.formatMessage({ id: "partners.subtitle" })}
            </p>
          </div>
          <div className="grid md:grid-cols-2 lg:grid-cols-4 gap-8">
            {partners.map((partner) => (
              <Card key={partner.id} className="border-0 shadow-lg">
                <CardContent className="p-6">
                  <div className="flex items-center justify-between mb-4">
                    <h3 className="text-lg font-semibold">{partner.name}</h3>
                    <div className="flex items-center">
                      <Star className="h-5 w-5 text-yellow-400 fill-current" />
                      <span className="ml-1 font-medium">
                        {partner.averageRating}
                      </span>
                    </div>
                  </div>
                  <div className="space-y-2 text-muted-foreground">
                    <div className="flex items-center">
                      <MapPin className="h-4 w-4 mr-2" />
                      <span>{partner.address}</span>
                    </div>
                    <div className="flex items-center">
                      <Users className="h-4 w-4 mr-2" />
                      <span>{partner.totalReviews} отзывов</span>
                    </div>
                    <div className="flex items-center">
                      <Phone className="h-4 w-4 mr-2" />
                      <span>{partner.phoneNumber}</span>
                    </div>
                  </div>
                  <div className="mt-4 space-y-2">
                    {partner.venues[0].services.map((service, index) => (
                      <Badge key={index} variant="secondary" className="mr-2">
                        {service.name}
                      </Badge>
                    ))}
                  </div>
                  <div className="mt-4 text-sm text-muted-foreground">
                    {partner.description}
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        </div>
      </section>

      {/* Partners Section */}
      <section className="py-20 bg-slate-50">
        <div className="container mx-auto px-4">
          <div className="text-center mb-16">
            <h2 className="text-3xl lg:text-4xl font-bold mb-4">
              {intl.formatMessage({ id: "partners.title" })}
            </h2>
            <p className="text-lg text-muted-foreground max-w-2xl mx-auto">
              {intl.formatMessage({ id: "partners.subtitle" })}
            </p>
          </div>
          <div className="grid md:grid-cols-2 lg:grid-cols-4 gap-8">
            {partners.map((partner) => (
              <Card
                key={partner.id}
                className="border-0 shadow-lg hover:shadow-xl transition-shadow duration-300"
              >
                <CardContent className="p-6">
                  <div className="flex items-center justify-between mb-4">
                    <div>
                      <h3 className="text-lg font-semibold">{partner.name}</h3>
                      <p className="text-sm text-muted-foreground">
                        {partner.fullName}
                      </p>
                    </div>
                    <div className="flex items-center">
                      <Star className="h-5 w-5 text-yellow-400 fill-current" />
                      <span className="ml-1 font-medium">
                        {partner.averageRating}
                      </span>
                    </div>
                  </div>

                  <div className="space-y-2 text-muted-foreground mb-4">
                    <div className="flex items-center">
                      <MapPin className="h-4 w-4 mr-2" />
                      <span>{partner.address}</span>
                    </div>
                    <div className="flex items-center">
                      <Phone className="h-4 w-4 mr-2" />
                      <span>{partner.phoneNumber}</span>
                    </div>
                    <div className="flex items-center">
                      <Users className="h-4 w-4 mr-2" />
                      <span>{partner.totalReviews} отзывов</span>
                    </div>
                  </div>

                  <div className="flex gap-2 mb-4">
                    <Badge
                      variant={partner.isOpen ? "success" : "destructive"}
                      className="flex items-center gap-1"
                    >
                      <div
                        className={`w-2 h-2 rounded-full ${
                          partner.isOpen ? "bg-green-500" : "bg-red-500"
                        }`}
                      />
                      {partner.isOpen ? "Открыто" : "Закрыто"}
                    </Badge>
                    {partner.hasParking && (
                      <Badge
                        variant="secondary"
                        className="flex items-center gap-1"
                      >
                        <Car className="h-3 w-3" />
                        Парковка
                      </Badge>
                    )}
                    {partner.hasWaitingRoom && (
                      <Badge
                        variant="secondary"
                        className="flex items-center gap-1"
                      >
                        <Users className="h-3 w-3" />
                        Комната ожидания
                      </Badge>
                    )}
                  </div>

                  <div className="mb-4">
                    <h4 className="text-sm font-semibold mb-2">
                      Режим работы:
                    </h4>
                    <div className="space-y-1 text-sm">
                      {partner.workingHours.map((wh, index) => (
                        <div
                          key={index}
                          className="flex justify-between items-center"
                        >
                          <span className="text-muted-foreground">
                            {wh.day}
                          </span>
                          {wh.isDayOff ? (
                            <span className="text-red-500">Выходной</span>
                          ) : (
                            <span>
                              {wh.openTime.slice(0, 5)} -{" "}
                              {wh.closeTime.slice(0, 5)}
                            </span>
                          )}
                        </div>
                      ))}
                    </div>
                  </div>

                  <div className="mb-4">
                    <h4 className="text-sm font-semibold mb-2">Услуги:</h4>
                    <div className="flex flex-wrap gap-2">
                      {partner.venues[0].services.map((service, index) => (
                        <Badge
                          key={index}
                          variant="secondary"
                          className="flex items-center gap-1"
                        >
                          <span>{service.name}</span>
                          <span className="text-xs text-muted-foreground">
                            {service.price.toLocaleString("ru-RU")} ₽
                          </span>
                        </Badge>
                      ))}
                    </div>
                  </div>

                  <p className="text-sm text-muted-foreground">
                    {partner.description}
                  </p>
                </CardContent>
              </Card>
            ))}
          </div>
        </div>
      </section>

      {/* Team Section */}
      <section className="py-20 bg-white">
        <div className="container mx-auto px-4">
          <div className="text-center mb-16">
            <h2 className="text-3xl lg:text-4xl font-bold mb-4">
              {intl.formatMessage({ id: "team.title" })}
            </h2>
            <p className="text-lg text-muted-foreground max-w-2xl mx-auto">
              {intl.formatMessage({ id: "team.subtitle" })}
            </p>
          </div>
          <div className="grid md:grid-cols-2 lg:grid-cols-4 gap-8">
            {team.map((member, index) => (
              <Card key={index} className="border-0 shadow-lg text-center">
                <CardContent className="p-6">
                  <div className="w-20 h-20 bg-gradient-to-br from-blue-100 to-purple-100 rounded-full mx-auto mb-4 flex items-center justify-center">
                    <Users className="h-10 w-10 text-blue-600" />
                  </div>
                  <h3 className="text-lg font-semibold mb-1">{member.name}</h3>
                  <div className="text-blue-600 mb-3">{member.role}</div>
                  <p className="text-muted-foreground">{member.description}</p>
                </CardContent>
              </Card>
            ))}
          </div>
        </div>
      </section>
    </div>
  );
}
