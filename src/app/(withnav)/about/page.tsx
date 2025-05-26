'use client'

import { Card } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import {
  Users,
  Building2,
  Trophy,
  Target,
  CheckCircle,
  Star,
  MapPin,
  Zap,
} from "lucide-react"
import { useIntl } from "react-intl"

export default function AboutPage() {
  const intl = useIntl()

  const stats = [
    {
      icon: <Users className="h-6 w-6" />,
      value: "50K+",
      label: intl.formatMessage({ id: "about.stats.clients" }),
    },
    {
      icon: <Building2 className="h-6 w-6" />,
      value: "500+",
      label: intl.formatMessage({ id: "about.stats.autoservices" }),
    },
    {
      icon: <Trophy className="h-6 w-6" />,
      value: "5+",
      label: intl.formatMessage({ id: "about.stats.experience" }),
    },
    {
      icon: <Target className="h-6 w-6" />,
      value: "99.9%",
      label: intl.formatMessage({ id: "about.stats.satisfaction" }),
    },
  ]

  const values = [
    {
      icon: <Star className="h-6 w-6 text-yellow-500" />,
      title: intl.formatMessage({ id: "about.values.quality.title" }),
      description: intl.formatMessage({ id: "about.values.quality.description" }),
    },
    {
      icon: <Zap className="h-6 w-6 text-blue-500" />,
      title: intl.formatMessage({ id: "about.values.innovation.title" }),
      description: intl.formatMessage({ id: "about.values.innovation.description" }),
    },
    {
      icon: <CheckCircle className="h-6 w-6 text-green-500" />,
      title: intl.formatMessage({ id: "about.values.reliability.title" }),
      description: intl.formatMessage({ id: "about.values.reliability.description" }),
    },
  ]

  const offices = [
    {
      city: "Bakı",
      address: "Nərimanov r., Heydər Əliyev pr. 152",
      type: intl.formatMessage({ id: "about.office.headquarters" }),
    },
    {
      city: "Gəncə",
      address: "Atatürk pr. 89",
      type: intl.formatMessage({ id: "about.office.regional" }),
    },
    {
      city: "Sumqayıt",
      address: "Sülh küç. 45",
      type: intl.formatMessage({ id: "about.office.regional" }),
    },
  ]

  return (
    <div className="min-h-screen bg-gradient-to-b from-slate-50 to-white pt-32">
      <div className="container mx-auto px-4">
        <div className="max-w-4xl mx-auto">
          {/* Hero Section */}
          <div className="text-center mb-16">
            <Badge variant="outline" className="mb-4">
              {intl.formatMessage({ id: "about.badge" })}
            </Badge>
            <h1 className="text-4xl font-bold mb-6">
              {intl.formatMessage({ id: "about.title" })}
            </h1>
            <p className="text-lg text-muted-foreground">
              {intl.formatMessage({ id: "about.description" })}
            </p>
          </div>

          {/* Stats Section */}
          <div className="grid sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-16">
            {stats.map((stat, index) => (
              <Card key={index} className="p-6 text-center">
                <div className="inline-flex items-center justify-center w-12 h-12 bg-blue-50 text-blue-600 rounded-lg mb-4">
                  {stat.icon}
                </div>
                <div className="text-3xl font-bold mb-2">{stat.value}</div>
                <div className="text-muted-foreground">{stat.label}</div>
              </Card>
            ))}
          </div>

          {/* Values Section */}
          <div className="mb-16">
            <h2 className="text-2xl font-bold text-center mb-8">
              {intl.formatMessage({ id: "about.values.title" })}
            </h2>
            <div className="grid md:grid-cols-3 gap-8">
              {values.map((value, index) => (
                <Card key={index} className="p-6">
                  <div className="inline-flex items-center justify-center w-12 h-12 bg-slate-100 rounded-lg mb-4">
                    {value.icon}
                  </div>
                  <h3 className="text-xl font-semibold mb-2">{value.title}</h3>
                  <p className="text-muted-foreground">{value.description}</p>
                </Card>
              ))}
            </div>
          </div>

          {/* Offices Section */}
          <div>
            <h2 className="text-2xl font-bold text-center mb-8">
              {intl.formatMessage({ id: "about.offices.title" })}
            </h2>
            <div className="grid md:grid-cols-3 gap-6">
              {offices.map((office, index) => (
                <Card key={index} className="p-6">
                  <div className="flex items-center mb-4">
                    <MapPin className="h-5 w-5 text-blue-500 mr-2" />
                    <h3 className="text-lg font-semibold">{office.city}</h3>
                  </div>
                  <p className="text-muted-foreground mb-3">{office.address}</p>
                  <Badge variant="secondary">{office.type}</Badge>
                </Card>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  )
} 