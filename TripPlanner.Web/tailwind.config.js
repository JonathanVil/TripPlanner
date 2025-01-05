/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    // other files...
    "./node_modules/flowbite/**/*.js"
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('flowbite/plugin')
  ],
}

