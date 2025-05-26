'use client'

import { Button } from "@/components/ui/button"
import { Card } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Textarea } from "@/components/ui/textarea"
import {
  Phone,
  Mail,
  MapPin,
  Clock,
  Send,
  MessageSquare,
  Headphones,
} from "lucide-react"
import { useIntl } from "react-intl"

export default function ContactPage() {
  const intl = useIntl()

  const contactInfo = [
    {
      icon: <Phone className="h-6 w-6" />,
      title: intl.formatMessage({ id: "contact.phone.title" }),
      value: intl.formatMessage({ id: "contacts.phone" }),
      description: intl.formatMessage({ id: "contact.phone.description" }),
    },
    {
      icon: <Mail className="h-6 w-6" />,
      title: intl.formatMessage({ id: "contact.email.title" }),
      value: intl.formatMessage({ id: "contacts.email" }),
      description: intl.formatMessage({ id: "contact.email.description" }),
    },
    {
      icon: <MapPin className="h-6 w-6" />,
      title: intl.formatMessage({ id: "contact.address.title" }),
      value: intl.formatMessage({ id: "contacts.address" }),
      description: intl.formatMessage({ id: "contact.address.description" }),
    },
    {
      icon: <Clock className="h-6 w-6" />,
      title: intl.formatMessage({ id: "contact.hours.title" }),
      value: intl.formatMessage({ id: "contacts.workHours" }),
      description: intl.formatMessage({ id: "contact.hours.description" }),
    },
  ]

  const offices = [
    {
      city: "Bakı",
      address: "Nərimanov r., Heydər Əliyev pr. 152",
      phone: "+994 12 555 00 11",
      email: "baku@arm-saas.az",
    },
    {
      city: "Gəncə",
      address: "Atatürk pr. 89",
      phone: "+994 22 555 00 22",
      email: "ganja@arm-saas.az",
    },
    {
      city: "Sumqayıt",
      address: "Sülh küç. 45",
      phone: "+994 18 555 00 33",
      email: "sumgait@arm-saas.az",
    },
  ]

  return (
    <div className="min-h-screen bg-gradient-to-b from-slate-50 to-white pt-32">
      <div className="container mx-auto px-4">
        <div className="max-w-4xl mx-auto">
          {/* Hero Section */}
          <div className="text-center mb-16">
            <h1 className="text-4xl font-bold mb-4">
              {intl.formatMessage({ id: "contact.title" })}
            </h1>
            <p className="text-lg text-muted-foreground">
              {intl.formatMessage({ id: "contact.description" })}
            </p>
          </div>

          {/* Contact Info Grid */}
          <div className="grid sm:grid-cols-2 gap-6 mb-16">
            {contactInfo.map((item, index) => (
              <Card key={index} className="p-6">
                <div className="flex items-start">
                  <div className="inline-flex items-center justify-center w-12 h-12 bg-blue-50 text-blue-600 rounded-lg mr-4">
                    {item.icon}
                  </div>
                  <div>
                    <h3 className="text-lg font-semibold mb-2">{item.title}</h3>
                    <p className="font-medium text-blue-600 mb-1">{item.value}</p>
                    <p className="text-muted-foreground">{item.description}</p>
                  </div>
                </div>
              </Card>
            ))}
          </div>

          {/* Contact Form */}
          <Card className="mb-16">
            <div className="p-8">
              <div className="flex items-center gap-3 mb-6">
                <MessageSquare className="h-6 w-6 text-blue-600" />
                <h2 className="text-2xl font-bold">
                  {intl.formatMessage({ id: "contact.form.title" })}
                </h2>
              </div>
              <div className="grid md:grid-cols-2 gap-6">
                <div>
                  <label className="block text-sm font-medium mb-2">
                    {intl.formatMessage({ id: "contact.form.name" })}
                  </label>
                  <Input placeholder={intl.formatMessage({ id: "contact.form.name.placeholder" })} />
                </div>
                <div>
                  <label className="block text-sm font-medium mb-2">
                    {intl.formatMessage({ id: "contact.form.email" })}
                  </label>
                  <Input placeholder={intl.formatMessage({ id: "contact.form.email.placeholder" })} type="email" />
                </div>
                <div className="md:col-span-2">
                  <label className="block text-sm font-medium mb-2">
                    {intl.formatMessage({ id: "contact.form.subject" })}
                  </label>
                  <Input placeholder={intl.formatMessage({ id: "contact.form.subject.placeholder" })} />
                </div>
                <div className="md:col-span-2">
                  <label className="block text-sm font-medium mb-2">
                    {intl.formatMessage({ id: "contact.form.message" })}
                  </label>
                  <Textarea
                    placeholder={intl.formatMessage({ id: "contact.form.message.placeholder" })}
                    rows={6}
                  />
                </div>
                <div className="md:col-span-2">
                  <Button className="w-full md:w-auto">
                    <Send className="h-4 w-4 mr-2" />
                    {intl.formatMessage({ id: "contact.form.submit" })}
                  </Button>
                </div>
              </div>
            </div>
          </Card>

          {/* Regional Offices */}
          <div>
            <div className="flex items-center gap-3 mb-8">
              <Headphones className="h-6 w-6 text-blue-600" />
              <h2 className="text-2xl font-bold">
                {intl.formatMessage({ id: "contact.offices.title" })}
              </h2>
            </div>
            <div className="grid md:grid-cols-3 gap-6">
              {offices.map((office, index) => (
                <Card key={index} className="p-6">
                  <h3 className="text-lg font-semibold mb-4">{office.city}</h3>
                  <div className="space-y-3 text-muted-foreground">
                    <div className="flex items-center gap-2">
                      <MapPin className="h-4 w-4" />
                      <span>{office.address}</span>
                    </div>
                    <div className="flex items-center gap-2">
                      <Phone className="h-4 w-4" />
                      <span>{office.phone}</span>
                    </div>
                    <div className="flex items-center gap-2">
                      <Mail className="h-4 w-4" />
                      <span>{office.email}</span>
                    </div>
                  </div>
                </Card>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
