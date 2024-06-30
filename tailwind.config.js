/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [],
  theme: {
    screens: {
      'mobile': '320px',
      'tablet': '640px',
      'desktop': '1024px',
    },
    extend: {
      colors: {
        customOrange: 'rgb(214, 106, 67)',
      },
      spacing: {
        '120': '120%',
        '900': '900px',
      },
      width: {
        'calc-33p-40': 'calc(33.33% - 40px)',
        'calc-50p-40': 'calc(50% - 40px)'
      },
    },
  },
  plugins: [],
}

