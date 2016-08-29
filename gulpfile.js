'use strict';

var gulp = require('gulp');
var clean = require('del');
var rename = require('gulp-rename');
var runSequence = require('run-sequence');
var cleanCss = require('gulp-clean-css');
var uglify = require('gulp-uglify');

gulp.task('clean', function () {
    return clean([
        './Content/dist/**/*'
    ]);
})

gulp.task('copy', function () {
    return gulp.src(['./Content/src/**/*', '!**/*.js', '!**/*.css'], { base: './Content/src' })
            .pipe(gulp.dest('./Content/dist'))
})

gulp.task('copyMinified', function () {
    return gulp.src(['./Content/src/**/*.min.js'], { base: './Content/src' })
            .pipe(gulp.dest('./Content/dist'))
})

gulp.task('minifyJs', function () {
    return gulp.src(['./Content/src/**/*.js', '!**/*min.js'], { base: './Content/src' })
            .pipe(uglify({ preserveComments: 'some' }))
            .pipe(rename({ suffix: '.min' }))
            .pipe(gulp.dest('./Content/dist'));
})

gulp.task('minifyCss', function () {
    return gulp.src(['./Content/src/**/*.css', '!**/*min.css'], { base: './Content/src' })
            .pipe(cleanCss())
            .pipe(rename({ suffix: '.min' }))
            .pipe(gulp.dest('./Content/dist'));
})

gulp.task('default', function (cb) {
    runSequence('clean', 'copy', 'copyMinified', ['minifyJs', 'minifyCss'], cb);
})