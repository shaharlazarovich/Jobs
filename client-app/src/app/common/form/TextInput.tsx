import React from 'react'
import {FieldRenderProps} from 'react-final-form'
import {FormFieldProps, Form, Label} from 'semantic-ui-react'

interface IProps extends FieldRenderProps<string, HTMLInputElement>,
            FormFieldProps {}

const TextInput: React.FC<IProps> = ({
    input,
    width,
    type,
    placeholder,
    meta: {touched, error}
}) => {
    //the double exclamation mark !!error will return boolean if 
    //there was an error- so when the textbox was touched 
    //an have an error - there will be red color
    return (
    <Form.Field error={touched && !!error} type={type} width={width}>
        <input {...input} placeholder={placeholder} />
        {touched && error && (
            <Label basic color='red'>
                {error}
            </Label>
        )}
    </Form.Field>
    );
};

export default TextInput
