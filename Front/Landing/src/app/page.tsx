'use client'

import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import {
  Car,
  Users,
  Target,
  Zap,
  Shield,
  BarChart3,
  Clock,
  CheckCircle,
  ArrowRight,
  Star,
  MapPin,
  Phone,
} from "lucide-react"
import { useIntl } from "react-intl"

export default function HomePage() {
  const intl = useIntl()

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
  ]

  const partners = [
    {
      name: intl.formatMessage({ id: "partners.list.0.name" }),
      location: intl.formatMessage({ id: "partners.list.0.location" }),
      services: intl.formatMessage({ id: "partners.list.0.services" }),
      rating: 4.9,
      clients: "500+",
    },
    {
      name: intl.formatMessage({ id: "partners.list.1.name" }),
      location: intl.formatMessage({ id: "partners.list.1.location" }),
      services: intl.formatMessage({ id: "partners.list.1.services" }),
      rating: 4.8,
      clients: "300+",
    },
    {
      name: intl.formatMessage({ id: "partners.list.2.name" }),
      location: intl.formatMessage({ id: "partners.list.2.location" }),
      services: intl.formatMessage({ id: "partners.list.2.services" }),
      rating: 4.9,
      clients: "400+",
    },
    {
      name: intl.formatMessage({ id: "partners.list.3.name" }),
      location: intl.formatMessage({ id: "partners.list.3.location" }),
      services: intl.formatMessage({ id: "partners.list.3.services" }),
      rating: 5.0,
      clients: "200+",
    },
  ]

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
  ]

  return (
    <div className="min-h-screen bg-gradient-to-b from-slate-50 to-white">
      {/* Hero Section */}
      <section className="relative py-20 lg:py-32 overflow-hidden">
        <div className="container mx-auto px-4 relative">
          <div className="max-w-4xl mx-auto text-center">
            <Badge variant="outline" className="mb-6 text-blue-600 border-blue-200">
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
              <Card key={index} className="border-0 shadow-lg hover:shadow-xl transition-shadow duration-300">
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
      <section className="py-20 bg-white">
        <div className="container mx-auto px-4">
          <div className="grid lg:grid-cols-2 gap-12 items-center">
            <div>
              <h2 className="text-3xl lg:text-4xl font-bold mb-6">
                {intl.formatMessage({ id: "mission.title" })}
              </h2>
              <p className="text-lg text-muted-foreground mb-6 leading-relaxed">
                {intl.formatMessage({ id: "mission.description" })}
              </p>
              <div className="space-y-4">
                <div className="flex items-center gap-3">
                  <CheckCircle className="h-5 w-5 text-green-600" />
                  <span>{intl.formatMessage({ id: "mission.point1" })}</span>
                </div>
                <div className="flex items-center gap-3">
                  <CheckCircle className="h-5 w-5 text-green-600" />
                  <span>{intl.formatMessage({ id: "mission.point2" })}</span>
                </div>
                <div className="flex items-center gap-3">
                  <CheckCircle className="h-5 w-5 text-green-600" />
                  <span>{intl.formatMessage({ id: "mission.point3" })}</span>
                </div>
              </div>
            </div>
            <div className="relative">
              <div className="aspect-square bg-gradient-to-br from-blue-100 to-purple-100 rounded-2xl flex items-center justify-center">
                <Target className="h-32 w-32 text-blue-600" />
              </div>
              <div className="absolute inset-0 flex items-center justify-center">
                <div className="text-center">
                  <h3 className="text-2xl font-bold mb-4">
                    {intl.formatMessage({ id: "vision.title" })}
                  </h3>
                  <p className="text-muted-foreground">
                    {intl.formatMessage({ id: "vision.description" })}
                  </p>
                </div>
              </div>
            </div>
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
            {partners.map((partner, index) => (
              <Card key={index} className="border-0 shadow-lg">
                <CardContent className="p-6">
                  <div className="flex items-center justify-between mb-4">
                    <h3 className="text-lg font-semibold">{partner.name}</h3>
                    <div className="flex items-center">
                      <Star className="h-5 w-5 text-yellow-400 fill-current" />
                      <span className="ml-1 font-medium">{partner.rating}</span>
                    </div>
                  </div>
                  <div className="space-y-2 text-muted-foreground">
                    <div className="flex items-center">
                      <MapPin className="h-4 w-4 mr-2" />
                      <span>{partner.location}</span>
                    </div>
                    <div className="flex items-center">
                      <Users className="h-4 w-4 mr-2" />
                      <span>{partner.clients} клиентов</span>
                    </div>
                  </div>
                  <Badge variant="secondary" className="mt-4">
                    {partner.services}
                  </Badge>
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
  )
}

