export class RequestLogs {
    RequestLogId?: number;
    Type?: string;
    TypeCode?: string;
    Description?: string;
    CreatedDate?: Date;
    File?: string;
    FileName?: string;
    FileExtension?: string;
    AppMessageId?: string;

    ResponseType?: string;
    RejectionReason?: string;
    ValidationErrors?: string;
    DocumentReferenceId?: number;

    AttachmentName?: string;
    AttachmentId?: string;
    AttachmentResponse?: RequestLogs[]
}
