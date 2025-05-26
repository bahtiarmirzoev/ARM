'use client'

import { IntlProvider } from 'react-intl'
import { createContext, useContext, useState } from 'react'
import azMessages from './messages/az'
import ruMessages from './messages/ru'
import enMessages from './messages/en'

const messages = {
  az: azMessages,
  ru: ruMessages,
  en: enMessages,
}

type Locale = keyof typeof messages

interface LocaleContextType {
  locale: Locale
  setLocale: (locale: Locale) => void
}

const LocaleContext = createContext<LocaleContextType | undefined>(undefined)

export function useLocale() {
  const context = useContext(LocaleContext)
  if (context === undefined) {
    throw new Error('useLocale must be used within a LocaleProvider')
  }
  return context
}

interface Props {
  children: React.ReactNode
}

export function I18nProvider({ children }: Props) {
  const [locale, setLocale] = useState<Locale>('az')

  return (
    <LocaleContext.Provider value={{ locale, setLocale }}>
      <IntlProvider
        messages={messages[locale]}
        locale={locale}
        defaultLocale="az"
      >
        {children}
      </IntlProvider>
    </LocaleContext.Provider>
  )
} 