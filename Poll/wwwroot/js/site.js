(function ($) {

    class PollForm {

        constructor($form) {

            this.$form = $form;

            this.submit = this.submit.bind(this);

            this.$form.submit(this.submit);

        }

        validate() {

            let result = true;

            this.$form.find('.question').each((index, question) => {

                let $question = $(question);

                $question.find('.error').remove();

                if (!this.validateQuestion($question.data('type'), $question)) {

                    result = false;
                    $question.prepend('<div class="error text-danger">Укажите ответ</div>');

                }

            });

            return result;

        }

        validateQuestion(qType, $qEle) {

            if (qType === 'Single') {
                return this.validateSingle($qEle);
            } else if (qType === 'Multi') {
                return this.validateMulti($qEle);
            }

        }

        validateSingle($qEle) {
            return $qEle.find('[type=radio]:checked').length > 0;
        }

        validateMulti($qEle) {
            return $qEle.find('[type=checkbox]:checked').length > 0;
        }

        submit(e) {

            e.preventDefault();

            this.$form.find('.response').remove();

            if (this.validate()) {

                $.post('home/SubmitPoll', this.$form.serialize())
                    .done(() => {
                        this.$form.prepend('<div class="response alert alert-success">Спасибо за ответы!</div>')
                    });
            }

            return false;

        }

    }

    new PollForm($('#poll-form'));

})(jQuery);