/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');

var prefixer = require("gulp-autoprefixer");
var minifyCss = require("gulp-minify-css");
var rename = require("gulp-rename");
var sass = require("gulp-sass");
var browserSync = require('browser-sync');
var bsName = 'bs';
var bs = browserSync.create(bsName);
var reload = bs.reload;
var paths = {
    sass: {
        main: 'Content/Scss/main.scss',
        watch: ['Content/Scss/main.scss', 'Content/Scss/**/*.scss'],
        dest: 'Content/Css',
    }
};

gulp.task('default', ['sass', 'serve']);

gulp.task('sass', function () {
    return gulp.src(paths.sass.main)
        .pipe(sass({ errLogToConsole: true }).on('error', sass.logError))
        .pipe(prefixer())
        .pipe(gulp.dest(paths.sass.dest))
        .pipe(bs.stream())
        .pipe(minifyCss({
            keepSpecialComments: 0
        }))
        .pipe(rename({ extname: '.min.css' }))
        .pipe(gulp.dest(paths.sass.dest));
});

gulp.task('serve', ['sass'], function () {
    bs.init({
        proxy: 'localhost:5034'
    });

    gulp.watch(paths.sass.watch, ['sass']);
    gulp.watch(paths.sass.watch).on('change', reload);
});